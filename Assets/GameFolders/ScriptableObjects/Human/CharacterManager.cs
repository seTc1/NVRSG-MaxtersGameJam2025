using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class CharacterManager : MonoBehaviour
{
    [Header("=== Data ===")]
    [SerializeField] private HumanData[] allCharactersData;

    [Header("=== Queue Settings ===")]
    [SerializeField] private Transform[] queuePoints;
    [SerializeField] private CharacterView characterViewPrefab;

    private List<CharacterInstance> activeInstances = new List<CharacterInstance>();
    private List<CharacterInstance> queueInstances = new List<CharacterInstance>();
    private List<CharacterView> queueViews = new List<CharacterView>();
    
     private PlayerStats_Manager _statsManager;

     [Inject]
     private void Construct(PlayerStats_Manager statsManager)
     {
         _statsManager = statsManager;
     }
    private void Start()
    {
        foreach (var data in allCharactersData)
            activeInstances.Add(new CharacterInstance(data));
        _statsManager._allPeople = activeInstances.Count;

        RefreshQueue();
    }

    private void Update()
    {
        for (int i = activeInstances.Count - 1; i >= 0; i--)
        {
            var inst = activeInstances[i];
            inst.Tick(Time.deltaTime);

            if (!inst.IsWorking && inst.IdleTimer <= 0f)
            {
                Remove(inst);
                continue;
            }

            if (inst.IsWorking && inst.BurnoutTimer <= 0f)
            {
                inst.AssignIdle();
            }
        }

        RefreshQueue();
        _statsManager._peopleAlive = activeInstances.Count;
        _statsManager._peopleWorking = activeInstances.Count(c => c.IsWorking);

    }

    public void CloseAllInfo()
    {
        foreach (CharacterView human in FindObjectsOfType<CharacterView>())
        {
            human.CloseInfoCanvas();
        }
    }

    public void RefreshQueue()
    {
        queueInstances = activeInstances
            .Where(c => !c.IsWorking && c.IdleTimer > 0f)
            .OrderBy(c => c.IdleTimer)
            .Take(queuePoints.Length)
            .ToList();

        while (queueViews.Count < queueInstances.Count)
        {
            var view = Instantiate(characterViewPrefab, queuePoints[queueViews.Count]);
            queueViews.Add(view);
        }

        while (queueViews.Count > queueInstances.Count)
        {
            Destroy(queueViews.Last().gameObject);
            queueViews.RemoveAt(queueViews.Count - 1);
        }

        for (int i = 0; i < queueInstances.Count; i++)
        {
            queueViews[i].Bind(queueInstances[i]);
            queueViews[i].transform.position = queuePoints[i].position;
        }
    }

    private void Remove(CharacterInstance inst)
    {
        activeInstances.Remove(inst);

        for (int i = queueViews.Count - 1; i >= 0; i--)
        {
            if (queueViews[i].GetInstance() == inst)
            {
                Destroy(queueViews[i].gameObject);
                queueViews.RemoveAt(i);
            }
        }
    }
}
