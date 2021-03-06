﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CustomEvent : UnityEvent<GameObject> {}

public class EventManager : Singleton<EventManager>
{
    private readonly Dictionary<string, CustomEvent> eventDictionary = new Dictionary<string, CustomEvent>();

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