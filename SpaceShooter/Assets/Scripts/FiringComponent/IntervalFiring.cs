using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalFiring : FiringComponent
{
    public override void Fire(Spaceship ship)
    {
        if (isFiring) return;
        
        waitTime += Time.deltaTime;
        if (waitTime > waveInterval)
        {
            isFiring = true;
            ship.StartCoroutine(ConsecutiveFiring(() =>
            {
                waitTime -= waveInterval;
                isFiring = false;
            },ship));
        }
    }

    IEnumerator ConsecutiveFiring(Action OnComplete, Spaceship ship)
    {
        for (int i = 0; i < consecutiveBullets; i++)
        {
            bullet.GenerateBullet(ship);
            yield return new WaitForSeconds(consecutiveInterval);
        }
        OnComplete();
    }
}