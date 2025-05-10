using System;
using UnityEngine;
using Zenject;

public class MonitorObject_Script : MonoBehaviour
{
    private PlayerInteraction_Script _playerInteractionScript;
    private SelectableObject _selectableObject;

    private void Start()
    {
        _selectableObject = GetComponent<SelectableObject>();
    }

    [Inject]
    private void Construct(PlayerInteraction_Script playerInteractionScript)
    {
        _playerInteractionScript = playerInteractionScript;
    }

    private void Update()
    {
        if (_playerInteractionScript.isLookingAtMonitors)
        {
            _selectableObject._canInteract = true;
        }
        else
        {
            _selectableObject._canInteract = false;
        }
    }
}
