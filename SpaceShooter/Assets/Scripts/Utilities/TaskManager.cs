using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TaskManager<TASK_TYPE> : Task where TASK_TYPE : Task
{
    protected readonly List<TASK_TYPE> Tasks = new List<TASK_TYPE>();
    private readonly List<TASK_TYPE> _pendingAdd = new List<TASK_TYPE>();
    private readonly List<TASK_TYPE> _pendingRemove = new List<TASK_TYPE>();

    public bool HasTasks => Tasks.Count > 0;
    public T GetTask<T>() where T: TASK_TYPE
    {
        foreach (var task in Tasks)
        {
            if (task.GetType() == typeof(T)) return (T)task;
        }
        return null;
    }
    public bool HasTask<T>() where T : TASK_TYPE
    {
        return GetTask<T>() != null;
    }

    public void Add(TASK_TYPE task)
    {
        SetStatus(TaskStatus.Working);
        task.SetStatus(TaskStatus.Pending);
        _pendingAdd.Add(task);
    }
    
    // only abort inner tasks, doesn't change the task status itself
    public void Clear()
    {
        foreach (var t in Tasks)
        {
            t.Abort();
        }
    }

    private void HandleCompletion(TASK_TYPE task)
    {
        _pendingRemove.Add(task);
        task.SetStatus(TaskStatus.Pending);
/*        if (Tasks.Count == 0)
        {
            SetStatus(TaskStatus.Success);
        }*/
    }

    internal override void Update()
    {
        OnUpdate();

        AddNewTasks(_pendingAdd);
        _pendingAdd.Clear();

        RemoveOldTasks(_pendingRemove);
        _pendingRemove.Clear();

        if (!HasTasks)
        {
            Debug.Log("Task Manager " + this + " is finished!");
            SetStatus(TaskStatus.Success);
        }
    }
    
    protected abstract void OnUpdate();
    
    protected virtual void AddNewTasks(IEnumerable<TASK_TYPE> newTasks)
    {
        foreach (var task in newTasks)
        {
            Tasks.Add(task);
        }
    }

    protected virtual void RemoveOldTasks(IEnumerable<TASK_TYPE> removedTasks)
    {
        foreach (var task in removedTasks)
        {
            Tasks.Remove(task);
        }
    }
    
    protected override void OnAbort()
    {
        Clear();
    }

    protected void ProcessTask(TASK_TYPE task)
    {
        if (task.IsPending)
        {
            task.SetStatus(TaskStatus.Working);
        }
        if (task.IsFinished)
        {
            HandleCompletion(task);
        }
        else
        {
            task.Update();
            if (task.IsFinished)
            {
                HandleCompletion(task);
            }
        }
    }
}

public class SerialTasks : TaskManager<Task>
{
    protected override void OnUpdate()
    {
        if (!HasTasks) return;
        var first = Tasks[0];
        ProcessTask(first);
    }
}

public class ParallelTasks : TaskManager<Task>
{
    protected override void OnUpdate()
    {
        foreach (var task in Tasks)
        {
            ProcessTask(task);
        }
    }
}

public class PriorityTask : Task
{
    public readonly int Priority = 0; // lower is higher priority (i.e. 1 is higher priority than 10)
}

public class PriorityTasks : TaskManager<PriorityTask>
{
    protected override void AddNewTasks(IEnumerable<PriorityTask> newTasks)
    {
        Tasks.AddRange(newTasks);
        Tasks.Sort((a, b) => a.Priority - b.Priority);
    }

    protected override void OnUpdate()
    {
        if (!HasTasks) return;
        var first = Tasks[0];
        ProcessTask(first);
    }
}