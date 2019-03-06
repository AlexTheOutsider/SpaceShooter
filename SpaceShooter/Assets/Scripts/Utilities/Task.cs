using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Task
{
    public enum TaskStatus : byte
    {
        Pending, // Task has not been initialized
        Working, // Task has been initialized
        Success, // Task completed successfully
        Failed, // Task completed unsuccessfully
        Aborted // Task was aborted
    }

    private TaskStatus _status = TaskStatus.Pending;
    public TaskStatus Status
    {
        get => _status;
        set => _status = value;
    }

    // Convenience status checking
    public bool IsPending => Status == TaskStatus.Pending;
    public bool IsWorking => Status == TaskStatus.Working;
    public bool IsSuccessful => Status == TaskStatus.Success;
    public bool IsFailed => Status == TaskStatus.Failed;
    public bool IsAborted => Status == TaskStatus.Aborted;
    public bool IsFinished => (Status == TaskStatus.Failed || Status == TaskStatus.Success || Status == TaskStatus.Aborted);

    internal void SetStatus(TaskStatus newStatus)
    {
        if (Status == newStatus) return;

        Status = newStatus;
        switch (newStatus)
        {
            case TaskStatus.Working:
                Init();
                break;
            case TaskStatus.Success:
                OnSuccess();
                CleanUp();
                break;
            case TaskStatus.Aborted:
                OnAbort();
                CleanUp();
                break;
            case TaskStatus.Failed:
                OnFail();
                CleanUp();
                break;
            case TaskStatus.Pending:
                break;
            default:
                throw new ArgumentOutOfRangeException(newStatus.ToString(), newStatus, null);
        }
    }
    
    // for external code to invoke
    public void Abort()
    {
        SetStatus(TaskStatus.Aborted);
    }

    protected virtual void OnAbort() {}
    protected virtual void OnSuccess() {}
    protected virtual void OnFail() {}
    protected virtual void Init() {}
    protected virtual void CleanUp() {}
    internal virtual void Update() {}
}