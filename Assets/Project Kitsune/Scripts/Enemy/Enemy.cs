using UnityEngine;

public class Enemy : Unit
{
    public Follower Follower { get; private set; }

    public Enemy(UnitInfo info, Rigidbody2D rigidbody)
    {
        Init(info);
        Follower = new Follower(GameLogic.Instance.Character.Transform, rigidbody, 1.2f);
    }

    protected override void OnUpdate(float deltaTime)
    {
        Follower.Movespeed = Attributes.Movespeed.Value;
        //Debug.Log("Follower: " + Follower.Movespeed + " (" + Attributes.Movespeed.Multiplier + ")");
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        Follower.FixedUpdate(deltaTime);
    }
}