using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MovementComponent
{
    private float newDirection = 1f;
    private float lastingTime;
    private float randomTime;
    private bool meetBoundary;

    public override void Start(Spaceship ship)
    {
        boundary = GameObject.Find("Boundary").GetComponent<DestroyByBoundary>().boundary;
        movement = ship.transform.forward;
    }

    public override void Update(Spaceship ship)
    {
        Movement(ship);
        BoundaryDetect(ship);
    }

    protected override void Movement(Spaceship ship)
    {
        lastingTime += Time.deltaTime;

        if (lastingTime > randomTime)
        {
            if (!meetBoundary)
                newDirection = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;

            movement = new Vector3(newDirection * steerFactor, 0f, -1f);
            lastingTime = 0f;
            randomTime = Random.Range(1f, maxTime);
            meetBoundary = false;
        }

        ship.GetComponent<Rigidbody>().velocity = movement * speed;
    }

    protected override void BoundaryDetect(Spaceship ship)
    {
        if (ship.transform.position.x < boundary.xMin)
        {
            newDirection = 1f;
            lastingTime = randomTime;
            meetBoundary = true;
        }
        else if (ship.transform.position.x > boundary.xMax)
        {
            newDirection = -1f;
            lastingTime = randomTime;
            meetBoundary = true;
        }
    }
}