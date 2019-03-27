using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletComponent
{
    public float speed = 10f;

    private Spaceship ship;

    protected void GiveSpeed(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * speed;
        obj.tag = ship.type == "Player" ? "Player" : "Enemy";
    }

    public virtual void GenerateBullet(Spaceship ship)
    {
        // so that this assignment will only be executed for once
        if (this.ship != null) return;
        
        this.ship = ship;
    }
}