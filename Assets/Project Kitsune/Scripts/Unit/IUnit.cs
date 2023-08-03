using System;

public interface IUnit
{
    public event Action OnLevelUp;
    public event Action OnDeath;

    public AttributesContainer Attributes { get; }
    public AbilitiesContainer Abilities { get; }
    public CursesContainer Curses { get; }
    public ModifiersContainer ModifiersContainer { get; }
    public UnitInfo UnitInfo { get; }

    public void Update(float deltaTime);
    public void FixedUpdate(float deltaTime);
    public void RegisterAbility(IAbility ability);
    public void DisableAbilities();
    public void TryRemoveField(IAbility ability);
    public void TakeDamage(float value, bool isProjectile);
    public void Immune(bool state);
}