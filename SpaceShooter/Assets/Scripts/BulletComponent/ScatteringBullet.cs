using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatteringBullet : BulletComponent
{
    public override void GenerateBullet(Spaceship ship)
    {
        GameObject bullet =
            Spaceship.Instantiate(Resources.Load("Prefabs/Bullet"), ship.transform.Find("Shot Point")) as GameObject;
        GameObject bulletLeft =
            Spaceship.Instantiate(Resources.Load("Prefabs/Bullet"), ship.transform.Find("Shot Point")) as GameObject;
        GameObject bulletRight =
            Spaceship.Instantiate(Resources.Load("Prefabs/Bullet"), ship.transform.Find("Shot Point")) as GameObject;

        bullet.transform.position = ship.transform.Find("Shot Point").position;
        bullet.transform.rotation = Quaternion.Euler(ship.transform.eulerAngles.x,ship.transform.eulerAngles.y,0f);
        
        bulletLeft.transform.position = ship.transform.Find("Shot Point").position;
        bulletLeft.transform.rotation = Quaternion.Euler(ship.transform.eulerAngles.x,ship.transform.eulerAngles.y+30,0f);
        
        bulletRight.transform.position = ship.transform.Find("Shot Point").position;
        bulletRight.transform.rotation = Quaternion.Euler(ship.transform.eulerAngles.x,ship.transform.eulerAngles.y-30,0f);

        GiveSpeed(bullet);
        GiveSpeed(bulletLeft);
        GiveSpeed(bulletRight);
    }
}