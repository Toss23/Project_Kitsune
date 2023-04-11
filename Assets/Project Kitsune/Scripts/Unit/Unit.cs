public abstract class Unit : IUnit
{
    public AttributesContainer Attributes { get; private set; }
    public AbilitiesState Abilities { get; private set; }

    protected void Init(UnitInfo info)
    {
        Attributes = new AttributesContainer(info);
        Attributes.Life.OnMinimum += Death;

        Abilities = new AbilitiesState(info.Abilities);
        Abilities.LevelUp((int)AbilityType.Attack);
    }

    public void Update(float deltaTime)
    {
        Attributes.Life.Regenerate(deltaTime);
        Abilities.UpdateCastTime(deltaTime);
    }

    public void RegisterAbility(IAbility ability)
    {
        ability.OnHit += OnHitAbility;
    }

    public void TakeDamage(float value)
    {
        Attributes.Life.Add(-value);
    }

    private void OnHitAbility(IAbility ability, IUnit target)
    {
        float damage = Damage.CalculateAbilityDamage(Attributes.Damage, ability, ability.Level);
        target.TakeDamage(damage);
    }

    // In Progress
    private void Death()
    {
        OnDeath();
    }
    
    protected abstract void OnDeath();
}