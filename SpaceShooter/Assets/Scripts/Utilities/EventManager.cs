using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<string, CustomEvent> eventDictionary = new Dictionary<string, CustomEvent>();
/*    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("can't find event manager!");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }*/

    public void StartListening(string eventName, UnityAction<GameObject> listener)
    {
        CustomEvent thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new CustomEvent ();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void StopListening(string eventName, UnityAction<GameObject> listener)
    {
        if (Instance == null) return;
        
        CustomEvent thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
    
    public void TriggerEvent(string eventName, GameObject obj = null)
    {
        CustomEvent thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(obj);
        }
    }
}

[Serializable]
public class CustomEvent : UnityEvent<GameObject>
{
    
}