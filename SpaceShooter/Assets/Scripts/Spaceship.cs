using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spaceship : MonoBehaviour
{
    public MovementComponent movementComponent;
    public FiringComponent firingComponent;
    public BulletComponent bulletComponent;
    public string type;

    public float xDegree, yDegree, zDegree;

    private void Awake()
    {
        if (type == "Player")
        {
            movementComponent = new ManualMovement();
            firingComponent = new NonstopFiring();
            bulletComponent = new NormalBullet();
        }
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
            firingComponent = new IntervalFiring();
            bulletComponent = new ScatteringBullet();
        }
    }

    private void Start()
    {
        movementComponent.Start(this);
        firingComponent.Start(this);
    }

    private void Update()
    {
        movementComponent.Update(this);
        firingComponent.Update(this);

        xDegree = transform.eulerAngles.x;
        yDegree = transform.eulerAngles.y;
        zDegree = transform.eulerAngles.z;
    }
}