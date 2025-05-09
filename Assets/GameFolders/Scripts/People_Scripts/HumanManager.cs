using System.Collections.Generic;
using UnityEngine;

public class HumansManager : MonoBehaviour
{
    [Header("=== Settings ===")]
    [SerializeField] private int humansCount = 100;
    [SerializeField] private GameObject humanPrefab;

    private List<Human> humans = new List<Human>();

    private void Start()
    {
        for (int i = 0; i < humansCount; i++)
        {
            float lifetime = Random.Range(60f, 300f);
            humans.Add(new Human(lifetime));
        }

        ShowSomeHumans(5);
    }

    private void Update()
    {
        foreach (var human in humans)
        {
            human.Update(Time.deltaTime);
        }
    }

    private void ShowSomeHumans(int count)
    {
        int shown = 0;
        foreach (var human in humans)
        {
            if (shown >= count) break;

            Vector3 position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            human.SetVisible(humanPrefab, position);
            shown++;
        }
    }
}