using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Boundary boundary;
    public float tilt = 30f;
    public float fireTime = 0.5f;

    private float waitTime;
    
    private void Update()
    {
        Movement();
        AutoFire();
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;
	
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );
	
        GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
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

[Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}