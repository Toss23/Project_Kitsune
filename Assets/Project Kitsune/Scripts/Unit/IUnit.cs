public interface IUnit
{
    public AttributesContainer Attributes { get; }
    public AbilitiesState Abilities { get; }

    public void Update(float deltaTime);
    public void RegisterAbility(IAbility ability);
    public void TakeDamage(float value);
}