﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NormalBullet : BulletComponent
{
    public override void GenerateBullet(Spaceship ship)
    {
        GameObject bullet =
            Spaceship.Instantiate(Resources.Load("Prefabs/Bullet"), ship.transform.Find("Shot Point")) as GameObject;
        bullet.transform.position = ship.transform.Find("Shot Point").position;
        bullet.transform.rotation = ship.transform.rotation;
        bullet.transform.rotation = Quaternion.Euler(ship.transform.eulerAngles.x,ship.transform.eulerAngles.y,0f);
        
        GiveSpeed(bullet);
    }
}