using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonstopFiring : MonoBehaviour
{
    public float fireTime = 0.5f;

    private float waitTime;

    void Update()
    {
        AutoFire();
    }
    
    private void AutoFire()
    {
        waitTime += Time.deltaTime;
        if (waitTime > fireTime)
        {
            GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullet"), transform.Find("Shot Point")) as GameObject;
            bullet.transform.position = transform.Find("Shot Point").position;
            waitTime -= fireTime;
        }
    }
}
