using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent_Manager : MonoBehaviour
{
    [System.Serializable]
    public class GameEvent
    {
        public int ID; // Уникальный идентификатор события
        public UnityEvent Event; // Событие, которое будет вызвано
    }

    public List<GameEvent> eventsList = new List<GameEvent>();

    public void TriggerEvent(int id)
    {
        // Поиск события с соответствующим ID 
        GameEvent gameEvent = eventsList.Find(e => e.ID == id);
        
        if (gameEvent != null)
        {
            gameEvent.Event?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Событие с ID {id} не найдено.");
        }
    }
}