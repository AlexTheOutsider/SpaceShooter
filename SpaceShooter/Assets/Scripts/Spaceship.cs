using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spaceship : MonoBehaviour
{
    public MovementComponent movementComponent;
    public string type;

    private void Awake()
    {
        if (type =="Player")
            movementComponent = new ManualMovement();
        else
        {
            switch (Random.Range(0,3))
            {
                case 0:
                    movementComponent = new RandomMovement();
                    break;
                case 1:
                    movementComponent = new FollowMovement();
                    break;
                case 2:
                    movementComponent = new ChargeMovement();
                    break;
            }
        }
        print("generate: "+movementComponent);
    }

    private void Start()
    {
        movementComponent.Start(this);
    }

    private void Update()
    {
        movementComponent.Update(this);
    }
}