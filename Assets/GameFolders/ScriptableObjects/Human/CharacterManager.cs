using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterManager : MonoBehaviour
{
    [Header("=== Data ===")]
    [SerializeField] private HumanData[] allCharacters;

    [Header("=== Queue ===")]
    [SerializeField] private Transform[] queuePoints;
    [SerializeField] private CharacterView characterViewPrefab;

    private List<CharacterInstance> activeCharacters = new List<CharacterInstance>();
    private List<CharacterInstance> queueCharacters = new List<CharacterInstance>();
    private List<CharacterView> activeViews = new List<CharacterView>();

    private void Start()
    {
        foreach (var data in allCharacters)
            activeCharacters.Add(new CharacterInstance(data));

        UpdateQueue();


    }

    private void Update()
    {
        for (int i = activeCharacters.Count - 1; i >= 0; i--)
        {
            var character = activeCharacters[i];
            character.Tick(Time.deltaTime);

            if (!character.IsWorking && character.IdleTimer <= 0f)
            {
                RemoveFromSystem(character);
                UpdateQueue();
                continue;
            }

            if (character.IsWorking && character.BurnoutTimer <= 0f)
            {
                character.IsWorking = false;
                if (!queueCharacters.Contains(character))
                    queueCharacters.Add(character);
            }
        }

        UpdateQueue();
    }

    private void UpdateQueue()
    {
        queueCharacters = queueCharacters
            .Where(c => c.IdleTimer > 0f && !c.IsWorking)
            .OrderBy(c => c.IdleTimer)
            .Take(queuePoints.Length)
            .ToList();

        while (activeViews.Count < queueCharacters.Count)
        {
            var view = Instantiate(characterViewPrefab, transform);
            activeViews.Add(view);
        }

        while (activeViews.Count > queueCharacters.Count)
        {
            Destroy(activeViews[activeViews.Count - 1].gameObject);
            activeViews.RemoveAt(activeViews.Count - 1);
        }

        for (int i = 0; i < queueCharacters.Count; i++)
        {
            activeViews[i].Bind(queueCharacters[i]);
            activeViews[i].transform.position = queuePoints[i].position;
        }
        foreach (var data in allCharacters)
        {
            var character = new CharacterInstance(data);
            activeCharacters.Add(character);
            queueCharacters.Add(character); // <- Добавляем сразу в очередь
        }
    }

    private void RemoveFromSystem(CharacterInstance character)
    {
        activeCharacters.Remove(character);
        queueCharacters.Remove(character);

        for (int i = activeViews.Count - 1; i >= 0; i--)
        {
            if (activeViews[i].GetInstance() == character)
            {
                Destroy(activeViews[i].gameObject);
                activeViews.RemoveAt(i);
            }
        }
    }
}
