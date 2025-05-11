using System;
using UnityEngine;
using Zenject;

public class PlayerStats_Manager : MonoBehaviour
{
    [SerializeField] public int _jobCardsCount;

    [SerializeField] public float _playerSecMaxTimer;
    [SerializeField] public float _currentTimer;

    [SerializeField] private int _peopleToLose;
    
    public int _allPeople;
    public int _peopleAlive;
    public int _peopleWorking;
    
    private GameState_Manager _gameStateManager;

    [Inject]
    private void Construct(GameState_Manager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }

    private void Start()
    {
        _currentTimer = _playerSecMaxTimer;
    }

    private void Update()
    {
        _currentTimer -= Time.deltaTime;
        if (_peopleAlive <= _peopleToLose)
        {
            _gameStateManager.LooseGame();
        }

        if (_currentTimer <= 0)
        {
            _gameStateManager.WinGame();
        }
    }
}
