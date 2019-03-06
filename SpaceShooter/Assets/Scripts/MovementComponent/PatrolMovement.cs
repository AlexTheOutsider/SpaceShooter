using UnityEngine;

public class PatrolMovement : MovementComponent
{
    public float patrolAltitude = 3f;
    
    private float newDirection = 1f;
    private float lastingTime;
    private float randomTime;
    private bool meetBoundary;

    public PatrolMovement(float altitude)
    {
        patrolAltitude = altitude;
    }
    
    public override void Start(Spaceship ship)
    {
        boundary = GameObject.Find("Boundary").GetComponent<DestroyByBoundary>().boundary;
        movement = ship.transform.forward;
        newDirection = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
    }

    public override void Update(Spaceship ship)
    {
        FindTarget(ship);
        Movement(ship);
        BoundaryDetect(ship);
    }

    protected override void Movement(Spaceship ship)
    {
        if (ship.transform.position.z < patrolAltitude)
        {
            movement = new Vector3(newDirection, 0f, 0f);
        }

        ship.GetComponent<Rigidbody>().velocity = movement * speed;
    }
    
    protected override void BoundaryDetect(Spaceship ship)
    {
        if (ship.transform.position.x < boundary.xMin)
        {
            newDirection = 1f;
            meetBoundary = true;
        }
        else if (ship.transform.position.x > boundary.xMax)
        {
            newDirection = -1f;
            meetBoundary = true;
        }
    }
}