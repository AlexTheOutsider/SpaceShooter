using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1 : Task
{
    public float duration = 10f;
    
    private readonly Boss boss;
    private float timeElasped;

    public Phase1(Boss boss)
    {
        this.boss = boss;
    }
    
    protected override void Init()
    {
        boss.movementComponent = new PatrolMovement(10f);
        boss.firingComponent = new IntervalFiring();
        boss.bulletComponent = new NormalBullet();
        
        boss.movementComponent.Start(boss);
        boss.firingComponent.Start(boss);
    }

    internal override void Update()
    {
/*        timeElasped += Time.deltaTime;
        if (timeElasped > duration)
        {
            SetStatus(TaskStatus.Success);
            return;
        }*/
        
        boss.movementComponent.Update(boss);
        boss.firingComponent.Update(boss);

        DetectHP();
    }

    private void DetectHP()
    {
        if (boss.hitPoints / boss.maxHP < 0.95f)
        {
            //Debug.Log("Switch Phase!");
            SetStatus(TaskStatus.Success);
        }
    }
}
