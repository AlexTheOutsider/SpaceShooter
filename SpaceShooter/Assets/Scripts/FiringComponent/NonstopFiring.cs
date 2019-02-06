using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonstopFiring : FiringComponent
{
    public override void Fire(Spaceship ship)
    {
        waitTime += Time.deltaTime;
        if (waitTime > waveInterval)
        {
            bullet.GenerateBullet(ship);
            waitTime -= waveInterval;
        }
    }
}
