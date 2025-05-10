using System;
using UnityEngine;

public class PlayerStats_Manager : MonoBehaviour
{
    [SerializeField] public int _jobCardsCount;

    [SerializeField] public float _playerSecMaxTimer;
    [SerializeField] public float _currentTimer;

    public int _allPeople;
    public int _peopleAlive;
    public int _peopleWorking;
    


    private void Start()
    {
        _currentTimer = _playerSecMaxTimer;
    }

    private void Update()
    {
        _currentTimer -= Time.deltaTime;
    }
}
