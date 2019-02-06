using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FiringComponent
{
    public int consecutiveBullets = 3;
    public float consecutiveInterval =0.1f;
    public float waveInterval = 0.5f;
    public BulletComponent bullet;
    
    protected float waitTime;
    protected bool isFiring;

    public void Start(Spaceship ship)
    {
        bullet = ship.bulletComponent;
    }

    public void Update(Spaceship ship)
    {
        Fire(ship);
    }

    public virtual void Fire(Spaceship ship){}
}
