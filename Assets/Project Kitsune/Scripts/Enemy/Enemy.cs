using UnityEngine;

public class Enemy : Unit
{
    public Follower Follower { get; private set; }

    public Enemy(UnitInfo info, Transform target, Rigidbody2D rigidbody)
    {
        Init(info);

        Follower = new Follower(target, rigidbody, 1, 1.2f);
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        Follower.FixedUpdate(deltaTime);
    }
}