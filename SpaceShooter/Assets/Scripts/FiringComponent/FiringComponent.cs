using System;

[Serializable]
public class FiringComponent
{
    public int consecutiveBullets = 3;
    public float consecutiveInterval =0.1f;
    public float waveInterval = 0.5f;
    
    protected float waitTime;
    protected bool isFiring;
    protected BulletComponent bullet;

    public void Start(Spaceship ship)
    {
        bullet = ship.bulletComponent;
    }

    public void Update(Spaceship ship)
    {
        Fire(ship);
    }

    protected virtual void Fire(Spaceship ship){}
}
