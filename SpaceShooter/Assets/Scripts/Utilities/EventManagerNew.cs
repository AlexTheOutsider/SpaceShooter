using System;
using System.Collections.Generic;

public abstract class MyEvent {}

// right now we only have one version of event manager,
// so we don't need to separate service interface and service provider class
// we may also want a logging decorator class to add log optionally
public class EventManagerNew : Singleton<EventManagerNew>
{
    private readonly Dictionary<Type, Action<MyEvent>> registeredHandlers = new Dictionary<Type, Action<MyEvent>>();
    private readonly List<MyEvent> queuedEvents = new List<MyEvent>();

    public void Register<T>(Action<MyEvent> handler) where T : MyEvent
    {
        Type type = typeof(T);
        if (registeredHandlers.ContainsKey(type))
        {
            registeredHandlers[type] += handler;
        }
        else
        {
            registeredHandlers[type] = handler;
        }
    }

    public void Unregister<T>(Action<MyEvent> handler) where T : MyEvent
    {
        Type type = typeof(T);
        Action<MyEvent> handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers -= handler;
            if (handlers == null)
            {
                registeredHandlers.Remove(type);
            }
            else
            {
                registeredHandlers[type] = handlers;
            }
        }
    }

    public void Fire(MyEvent e)
    {
        Type type = e.GetType();
        Action<MyEvent> handlers;
        if (registeredHandlers.TryGetValue(type, out handlers))
        {
            handlers(e);
        }
    }

    public void QueueEvent(MyEvent e) {
        queuedEvents.Insert(0, e);
    }

    public void ProcessQueuedEvents() {
        for (int i = queuedEvents.Count - 1; i >= 0; --i) {
            Fire(queuedEvents[i]);
            queuedEvents.RemoveAt(i);
        }
    }
}