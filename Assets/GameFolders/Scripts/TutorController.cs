using System;
using UnityEngine;
using Zenject;

public class TutorController : MonoBehaviour
{
    private bool _printerTutor;
    private bool _chairTutor;
    
    private PlayerStats_Manager _playerStats;
    private GameEvent_Manager _gameEvent;
    private PlayerInteraction_Script _playerInteractionScript;

    [Inject]
    public void Construct(PlayerStats_Manager playerStats, GameEvent_Manager gameEvent, PlayerInteraction_Script playerInteractionScript)
    {
        _playerStats = playerStats;
        _gameEvent = gameEvent;
        _playerInteractionScript = playerInteractionScript;
        
    }

    private void Update()
    {
        if (!_printerTutor && _playerStats._jobCardsCount == 5)
        {
            _printerTutor = true;
            _gameEvent.TriggerEvent(1);
        }

        if (_printerTutor && !_chairTutor && _playerInteractionScript.isLookingAtMonitors)
        {
            _chairTutor = true;
            _gameEvent.TriggerEvent(3);
        }
    }
}
