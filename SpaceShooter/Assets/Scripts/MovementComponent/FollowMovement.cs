using UnityEngine;

public class FollowMovement : MovementComponent
{
    public override void Update(Spaceship ship)
    {
        FindTarget(ship);
        Movement(ship);
    }

    protected override void Movement(Spaceship ship)
    {
        if (player.position.z > ship.transform.position.z)
        {
            movement = ship.transform.forward;
        }
        else
        {
            movement = player.position - ship.transform.position;
            movement = new Vector3(Mathf.Clamp(movement.x, -1f, 1f) * steerFactor, 0f, -1f);
        }

        ship.GetComponent<Rigidbody>().velocity = movement * speed;
    }
}