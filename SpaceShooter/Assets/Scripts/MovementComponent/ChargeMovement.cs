using UnityEngine;

public class ChargeMovement : MovementComponent
{
    public override void Start(Spaceship ship)
    {
        FindTarget(ship);
        Movement(ship);
    }

    protected override void Movement(Spaceship ship)
    {
        movement = player.position - ship.transform.position;
        ship.GetComponent<Rigidbody>().velocity = movement.normalized * speed;
    }
}