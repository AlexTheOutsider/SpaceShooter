using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public MovementComponent movementComponent;
    public FiringComponent firingComponent;
    public BulletComponent bulletComponent;
    public string type;

    private void Awake()
    {
        RandomGenerate();
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
    }

    public Spaceship(MovementComponent move, FiringComponent fire, BulletComponent bullet)
    {
        movementComponent = move;
        firingComponent = fire;
        bulletComponent = bullet;
    }
    
    private void RandomGenerate()
    {
        if (type == "Player")
        {
            movementComponent = new ManualMovement();
            firingComponent = new NonstopFiring();
            bulletComponent = new NormalBullet();
            gameObject.tag = "Player";
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
            switch (Random.Range(0, 2))
            {
                case 0:
                    bulletComponent = new NormalBullet();
                    break;
                case 1:
                    bulletComponent = new ScatteringBullet();;
                    break;
            }
            gameObject.tag = "Enemy";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boundary") return;
        if (gameObject.tag == other.gameObject.tag) return;

        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}