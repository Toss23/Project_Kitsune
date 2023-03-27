public class Character : ICharacter
{
    public Controlable Controlable { get; }

    public Character()
    {
        Controlable = new Controlable();
        Controlable.Speed = 3;
    }
}