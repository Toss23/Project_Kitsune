using UnityEngine;

public class Character : Unit
{
    public Controlable Controlable { get; private set; }

    public Character(UnitInfo info, Rigidbody2D rigidbody)
    {
        Init(info);
        Controlable = new Controlable(rigidbody, 3);
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        Controlable.FixedUpdate(deltaTime);
    }
}