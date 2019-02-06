using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletComponent
{
    public float speed = 10f;
    
    public void GiveSpeed(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * speed;
    }

    public virtual void GenerateBullet(Spaceship ship){}
}
