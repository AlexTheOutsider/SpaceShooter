using System;

// Basic Task
/// This task will run the provided action endlessly
/// (or until it or the collection it's in is stopped)
public class BasicTask : Task
{
    private readonly Action _work;

    public BasicTask(Action work)
    {
        _work = work;
    }

    internal override void Update()
    {
        _work();
    }
}
