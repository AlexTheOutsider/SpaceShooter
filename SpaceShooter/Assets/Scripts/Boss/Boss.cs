using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Spaceship
{
    private readonly SerialTasks phases = new SerialTasks();

    protected override void Awake()
    {
    }

    private void Start()
    {
        hitPoints = maxHP;
        phases.Add(new Phase1(this));
        phases.Add(new Phase2(this));
    }

    private void Update()
    {
        if (phases.IsFinished)
        {
            OnKilled();
            return;
        }
        phases.Update();
    }

    private void OnKilled()
    {
        print("Boss is killed!");
    }
}
