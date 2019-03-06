using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : Task
{
    public float duration = 10f;
    
    private readonly Boss boss;
    private float timeElasped;

    public Phase2(Boss boss)
    {
        this.boss = boss;
    }
    
    protected override void Init()
    {
        boss.movementComponent = new PatrolMovement(3f);
        boss.firingComponent = new IntervalFiring();
        boss.bulletComponent = new ScatteringBullet();
        boss.tag = "Enemy";
        
        boss.movementComponent.Start(boss);
        boss.firingComponent.Start(boss);
    }

    internal override void Update()
    {
        timeElasped += Time.deltaTime;
        if (timeElasped > duration)
        {
            SetStatus(TaskStatus.Success);
            return;
        }
        
        boss.movementComponent.Update(boss);
        boss.firingComponent.Update(boss);
    }
}
