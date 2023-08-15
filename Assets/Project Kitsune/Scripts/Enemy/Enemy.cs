using UnityEngine;

public class Enemy : Unit
{
    public Follower Follower { get; private set; }

    public Enemy(UnitInfo info, IUnitPresenter unitPresenter, Rigidbody2D rigidbody)
    {
        Init(info, unitPresenter);
        Follower = new Follower(GameLogic.Instance.Character.Transform, rigidbody, 1.5f);
    }

    protected override void OnUpdate(float deltaTime)
    {
        Follower.Movespeed = Attributes.Movespeed.Value;
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        Follower.FixedUpdate(deltaTime);
    }
}