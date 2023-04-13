using System;
using System.Collections.Generic;

public abstract class Unit : IUnit
{
    public event Action<float> OnLevelUp;
    public event Action OnDeath;

    public AttributesContainer Attributes { get; private set; }
    public AbilitiesState Abilities { get; private set; }

    private List<IAbility> _castedAbilities;

    protected void Init(UnitInfo info)
    {
        _castedAbilities = new List<IAbility>();

        Attributes = new AttributesContainer(info);
        Attributes.Life.OnMinimum += Death;
        Attributes.Level.OnLevelUp += LevelUp;

        Abilities = new AbilitiesState(info.Abilities);
        Abilities.LevelUp((int)AbilityType.Attack);
    }

    public void Update(float deltaTime)
    {
        Attributes.Life.Regenerate(deltaTime);
        Abilities.UpdateCastTime(deltaTime);
        OnUpdate(deltaTime);
    }   

    public void FixedUpdate(float deltaTime)
    {
        OnFixedUpdate(deltaTime);
    }

    protected abstract void OnFixedUpdate(float deltaTime);
    protected abstract void OnUpdate(float deltaTime);

    private void LevelUp(float level)
    {
        OnLevelUp?.Invoke(level);
    }

    public void RegisterAbility(IAbility ability)
    {
        ability.OnHit += OnHitAbility;
        _castedAbilities.Add(ability);
    }

    public void DisableAbilities()
    {
        foreach (IAbility ability in _castedAbilities)
        {
            ability.OnHit -= OnHitAbility;
        }
    }

    public void TakeDamage(float value)
    {
        Attributes.Life.Add(-value);
    }

    private void OnHitAbility(IAbility ability, IUnit target)
    {
        float damage = Damage.CalculateAbilityDamage(Attributes.Damage, ability, ability.Level);
        target.TakeDamage(damage);
        ability.OnHit -= OnHitAbility;
    }

    private void Death()
    {
        OnDeath?.Invoke();
    }
}