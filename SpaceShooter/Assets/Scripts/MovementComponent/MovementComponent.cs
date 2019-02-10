using System;
using UnityEngine;

[Serializable]
public class MovementComponent
{
    public float speed = 3f;
    public float steerFactor = 0.5f;
    public float maxTime = 5f;
    public float tilt = 30f;
    
    protected Vector3 movement;
    protected Transform player;
    protected Boundary boundary;
    
    public virtual void Start(Spaceship ship) {}

    public virtual void Update(Spaceship ship) {}

    protected virtual void Movement(Spaceship ship) {}

    protected virtual void BoundaryDetect(Spaceship ship) {}

    protected void FindTarget(Spaceship ship)
    {
        player = GameObject.FindWithTag("Player").transform;
    }
}