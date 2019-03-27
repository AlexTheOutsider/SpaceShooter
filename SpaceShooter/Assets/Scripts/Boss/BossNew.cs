using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNew : Spaceship
{
    private Tree<BossNew> _tree;
    
    // override the parent's Awake function to not random generate attributes
    protected override void Awake()
    {
    }
    
    private void Start()
    {
        hitPoints = maxHP;
        firingComponent = new IntervalFiring();
        bulletComponent = new ScatteringBullet();
        tag = "Boss";

        firingComponent.Start(this);

        _tree = new Tree<BossNew>(new Selector<BossNew>(
            new Sequence<BossNew>(
                new Not<BossNew>(new IsLowHealth()),new Patrol(),new Shoot()),
            new Sequence<BossNew>(
                new IsLowHealth(),new Charge())));
    }

    private void Update()
    {
        _tree.Update(this);
    }

    private class IsLowHealth : Node<BossNew>
    {
        public override bool Update(BossNew context)
        {
            return (context.hitPoints / context.maxHP) < 0.3f;
        }
    }

    private class Patrol : Node<BossNew>
    {
        public override bool Update(BossNew context)
        {
            if (!(context.movementComponent is PatrolMovement))
            {
                context.movementComponent = new PatrolMovement(10f);
                context.movementComponent.Start(context);
            }
            context.movementComponent.Update(context);
            return true;
        }
    }

    private class Charge : Node<BossNew>
    {
        public override bool Update(BossNew context)
        {
            if (!(context.movementComponent is ChargeMovement))
            {
                context.movementComponent = new ChargeMovement();
                context.movementComponent.Start(context);
            }
            context.movementComponent.Update(context);
            return true;
        }
    }
    
    private class Shoot : Node<BossNew>
    {
        public override bool Update(BossNew context)
        {
            context.firingComponent.Update(context);
            return true;
        }
    }
}