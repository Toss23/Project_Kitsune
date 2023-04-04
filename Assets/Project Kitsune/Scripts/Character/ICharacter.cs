public interface ICharacter
{
    public Controlable Controlable { get; }
    public CharacterAttributes Attributes { get; }
    public AbilitiesState Abilities { get; }

    public void Update(float deltaTime);
    public void RegisterAbility(IAbility ability);
}