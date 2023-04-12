public class Character : Unit
{
    public Controlable Controlable { get; private set; }

    public Character(UnitInfo info)
    {
        Init(info);

        Controlable = new Controlable();
        Controlable.Speed = 3;
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }

    protected override void OnFixedUpdate(float deltaTime)
    {
        
    }
}