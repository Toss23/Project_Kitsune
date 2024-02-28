using UnityEngine;

public class Character : Unit
{
    public Controllable Controllable { get; private set; }

    public Character(UnitInfo info, IUnitPresenter unitPresenter, Rigidbody2D rigidbody)
    {
        Init(info, unitPresenter);
        Controllable = new Controllable(rigidbody);
    }

    protected override void OnUpdate(float deltaTime)
    {
        Controllable.Movespeed = AttributesContainer.Movespeed.Value;
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        Controllable.FixedUpdate(deltaTime);
    }
}