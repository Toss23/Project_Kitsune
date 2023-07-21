using System;
using System.Collections.Generic;

public abstract class Unit : IUnit
{
    public event Action OnLevelUp;
    public event Action OnDeath;

    public AttributesContainer Attributes { get; private set; }
    public AbilitiesContainer Abilities { get; private set; }
    public CursesContainer Curses { get; private set; }
    public float ExperienceGain { get; private set; }

    private List<IAbility> _castedAbilities;
    private bool _isImmune = false;

    protected void Init(UnitInfo info)
    {
        _castedAbilities = new List<IAbility>();

        Attributes = new AttributesContainer(info);
        Attributes.Life.OnMinimum += Death;
        Attributes.Level.OnLevelUp += LevelUp;

        Abilities = new AbilitiesContainer(info.Abilities, info.AttackAnimationTime);

        Curses = new CursesContainer();

        ExperienceGain = info.ExperienceGain;
    }

    public void Update(float deltaTime)
    {
        Attributes.Life.Regenerate(deltaTime);
        Attributes.MagicShield.Regenerate(deltaTime);
        Abilities.UpdateCastTime(deltaTime);
        Curses.Update(deltaTime);
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
        OnLevelUp?.Invoke();
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

    public void TakeDamage(float value, bool isProjectile)
    {
        if (Curses.Have(CursesInfo.List.Weakness))
        {
            Curse weakness = Curses.Find(CursesInfo.List.Weakness);
            value *= (1 + weakness.Effect / 100f) * CursesInfo.Weakness.InputDamageMultiplier;
        }

        if (_isImmune == false)
        {
            if (isProjectile)
            {
                Attributes.Life.Add(-value);
            }
            else
            {
                float pool = Attributes.MagicShield.Value - value;
                if (pool >= 0)
                {
                    Attributes.MagicShield.Add(-value);
                }
                else
                {
                    Attributes.MagicShield.Set(0);
                    Attributes.Life.Add(pool);
                }
            }
        }
    }

    public void Immune(bool state)
    {
        _isImmune = state;
    }

    private void OnHitAbility(IAbility ability, IUnit target)
    {
        if (target != null)
        {
            float damage = Damage.CalculateAbilityDamage(Attributes.Damage, ability, ability.Level);
            bool isProjectile = ability.Info.AbilityType == AbilityInfo.Type.Projectile;

            if (Curses.Have(CursesInfo.List.Weakness))
            {
                Curse weakness = Curses.Find(CursesInfo.List.Weakness);
                damage *= weakness.Effect / 100f * CursesInfo.Weakness.OutputDamageMultiplier;
            }

            target.TakeDamage(damage, isProjectile);
        }
    }

    private void Death()
    {
        OnDeath?.Invoke();
    }
}