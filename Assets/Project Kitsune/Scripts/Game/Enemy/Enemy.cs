using UnityEngine;

public class Enemy : Unit
{
    public Follower Follower { get; private set; }

    public Enemy(IContext logic, UnitInfo info, IUnitPresenter unitPresenter, Rigidbody2D rigidbody)
    {
        Init(info, unitPresenter);
        Follower = new Follower(logic, rigidbody, 1.5f);
    }

    protected override void OnUpdate(float deltaTime)
    {
        Follower.Movespeed = AttributesContainer.Movespeed.Value;
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        Follower.FixedUpdate(deltaTime);
    }
}