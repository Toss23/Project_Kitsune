using System;

public interface IUnit
{
    public event Action OnDeath;

    public AttributesContainer Attributes { get; }
    public AbilitiesState Abilities { get; }

    public void Update(float deltaTime);
    public void FixedUpdate(float deltaTime);
    public void RegisterAbility(IAbility ability);
    public void DisableAbilities();
    public void TakeDamage(float value);
}