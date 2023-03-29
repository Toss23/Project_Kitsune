public interface ICharacter
{
    public Controlable Controlable { get; }
    public CharacterAttributes Attributes { get; }

    public void Update(float deltaTime);
}