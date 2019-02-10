using System;
using System.Collections;
using UnityEngine;

public class IntervalFiring : FiringComponent
{
    protected override void Fire(Spaceship ship)
    {
        if (isFiring) return;

        waitTime += Time.deltaTime;
        if (waitTime > waveInterval)
        {
            isFiring = true;
            ship.StartCoroutine(ConsecutiveFiring(ship, () =>
            {
                waitTime -= waveInterval;
                isFiring = false;
            }));
        }
    }

    IEnumerator ConsecutiveFiring(Spaceship ship, Action OnComplete)
    {
        for (int i = 0; i < consecutiveBullets; i++)
        {
            bullet.GenerateBullet(ship);
            yield return new WaitForSeconds(consecutiveInterval);
        }

        OnComplete();
    }
}