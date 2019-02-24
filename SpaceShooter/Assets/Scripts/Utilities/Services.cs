using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class Services
{
    private static EventManagerNew _eventManagerNew;
    public static EventManagerNew EventManagerNew
    {
        get
        {
            // or you can use null service instead
            Debug.Assert(_eventManagerNew != null);
            return _eventManagerNew;
        }
        set => _eventManagerNew = value;
    }
}
