using UnityEngine;

public class NonstopFiring : FiringComponent
{
    protected override void Fire(Spaceship ship)
    {
        waitTime += Time.deltaTime;
        if (waitTime > waveInterval)
        {
            bullet.GenerateBullet(ship);
            waitTime -= waveInterval;
        }
    }
}