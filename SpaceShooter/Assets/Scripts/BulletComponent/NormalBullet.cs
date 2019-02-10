using UnityEngine;

public class NormalBullet : BulletComponent
{
    public override void GenerateBullet(Spaceship ship)
    {
        base.GenerateBullet(ship);
        
        GameObject bullet =
            Spaceship.Instantiate(Resources.Load("Prefabs/Bullet"), GameObject.Find("Bullets").transform) as GameObject;
        bullet.transform.position = ship.transform.Find("Shot Point").position;
        bullet.transform.rotation = Quaternion.Euler(ship.transform.eulerAngles.x,ship.transform.eulerAngles.y,0f);
        
        GiveSpeed(bullet);
    }
}