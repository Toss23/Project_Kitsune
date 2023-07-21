using UnityEngine;

public class Character : Unit
{
    public Controllable Controllable { get; private set; }

    public Character(UnitInfo info, Rigidbody2D rigidbody)
    {
        Init(info);
        Controllable = new Controllable(rigidbody, 3);
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        Controllable.FixedUpdate(deltaTime);
    }
}