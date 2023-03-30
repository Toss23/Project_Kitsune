public class Character : ICharacter
{
    public Controlable Controlable { get; }
    public CharacterAttributes Attributes { get; }

    public Character(CharacterInfo characterInfo)
    {
        Controlable = new Controlable();
        Controlable.Speed = 3;

        Attributes = new CharacterAttributes(characterInfo);
        Attributes.Life.OnMinimum += Death;
    }

    public void Update(float deltaTime)
    {
        Attributes.Life.Regenerate(deltaTime);
    }

    private void Death()
    {
        Attributes.Life.Regeneration = 0;
    }
}