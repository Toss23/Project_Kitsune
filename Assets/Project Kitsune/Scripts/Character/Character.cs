public class Character : ICharacter
{
    public Controlable Controlable { get; }
    public CharacterAttributes Attributes { get; }

    public Character()
    {
        Controlable = new Controlable();
        Controlable.Speed = 3;

        Attributes = new CharacterAttributes();
        Attributes.Life.OnMinimum += Death;

        // For test
        Attributes.Life.Set(50);
        Attributes.Life.Regeneration = 10;
    }

    public void Update(float deltaTime)
    {
        Attributes.Life.Regenerate(deltaTime);
    }

    private void Death()
    {

    }
}