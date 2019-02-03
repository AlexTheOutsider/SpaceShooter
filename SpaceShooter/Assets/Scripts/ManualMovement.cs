using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualMovement : MovementComponent
{
    public override void Start(Spaceship ship)
    {
        boundary = GameObject.Find("Boundary").GetComponent<DestroyByBoundary>().boundary;
    }
    
    public override void Update(Spaceship ship)
    {
        Movement(ship);
    }
    
    protected override void Movement(Spaceship ship)
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        ship.GetComponent<Rigidbody>().velocity = movement * speed;
	
        ship.GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp (ship.GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp (ship.GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );
	
        ship.GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, ship.GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}