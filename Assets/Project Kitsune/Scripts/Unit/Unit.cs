using System;
using System.Collections.Generic;

public abstract class Unit
{
    public event Action OnLevelUp;
    public event Action OnDeath;
    public event Action<float, Unit> OnDealDamage;

    private UnitInfo _unitInfo;
    private IUnitPresenter _unitPresenter;
    private List<IAbility> _castedAbilities;
    private bool _isImmune = false;

    public AttributesContainer Attributes { get; private set; }
    public AbilitiesContainer Abilities { get; private set; }
    public CursesContainer Curses { get; private set; }
    public ModifiersContainer ModifiersContainer { get; private set; }
    public UnitInfo UnitInfo => _unitInfo;
    public IUnitPresenter UnitPresenter => _unitPresenter;

    protected void Init(UnitInfo unitInfo, IUnitPresenter unitPresenter)
    {
        _unitInfo = unitInfo;
        _unitPresenter = unitPresenter;
        _castedAbilities = new List<IAbility>();

        ModifiersContainer = new ModifiersContainer(Attributes);

        Attributes = new AttributesContainer(this);
        Attributes.Life.OnMinimum += Death;
        Attributes.Level.OnLevelUp += LevelUp;

        Abilities = new AbilitiesContainer(this, unitInfo.Abilities, ModifiersContainer.AbilityModifiers);

        Curses = new CursesContainer();
        Curses.OnCursed += OnCursed;
        Curses.OnCurseCleared += OnCurseCleared;
    }

    public void Update(float deltaTime)
    {
        Attributes.Update(deltaTime);
        Abilities.Update(deltaTime);
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
        ability.OnHit += DealDamage;
        _castedAbilities.Add(ability);
    }

    public void TryRemovePassiveAbility(IAbility ability)
    {
        IAbility passive = _castedAbilities.Find(item => item.AbilityData.Name == ability.AbilityData.Name);
        if (passive != null)
        {
            _castedAbilities.Remove(passive);
            passive.DestroyAbility();
        }
    }

    public void DisableAbilities()
    {
        foreach (IAbility ability in _castedAbilities)
        {
            ability.OnHit -= DealDamage;
        }
    }

    public void TakeDamage(float value, bool isProjectile)
    {
        if (Curses.Have(CursesInfo.List.Weakness))
        {
            Curse weakness = Curses.Find(CursesInfo.List.Weakness);
            value *= (1 + weakness.Effect / 100f) * CursesInfo.Weakness.InputDamageMultiplier;
        }

        value *= (1 - _unitInfo.Armour / 100f);

        if (_isImmune == false)
        {
            if (isProjectile)
            {
                Attributes.Life.TakeDamage(value);
            }
            else
            {
                float excess = Attributes.MagicShield.TakeDamage(value);
                Attributes.Life.TakeDamage(excess);
            }
        }
    }

    public void Immune(bool state)
    {
        _isImmune = state;
    }

    private void DealDamage(IAbility ability, Unit target)
    {
        if (target != null)
        {
            float damage = Damage.CalculateAbilityDamage(Attributes.Damage, ability, ability.Level);
            bool isProjectile = ability.AbilityData.GetAbilityType() == AbilityData.Type.Range;

            if (Curses.Have(CursesInfo.List.Weakness))
            {
                Curse weakness = Curses.Find(CursesInfo.List.Weakness);
                damage *= weakness.Effect / 100f * CursesInfo.Weakness.OutputDamageMultiplier;
            }

            if (damage != 0)
            {
                target.TakeDamage(damage, isProjectile);
                OnDealDamage?.Invoke(damage, target);
            }
        }
    }

    private void OnCursed(Curse curse)
    {
        /*
        if (curse.Name == CursesInfo.List.Forest)
        {
            ModifiersContainer.Add(new AttributeModifier()
            {
                Movespeed = 1 - CursesInfo.Forest.ActionSpeedMultiplier * curse.Effect / 100,
                ActionSpeed = 1 - CursesInfo.Forest.ActionSpeedMultiplier * curse.Effect / 100
            });
        }*/
    }

    private void OnCurseCleared(Curse curse)
    {
        /*
        if (curse.Name == CursesInfo.List.Forest)
        {
            ModifiersContainer.Remove(new AttributeModifier()
            {
                Movespeed = 1 - CursesInfo.Forest.ActionSpeedMultiplier * curse.Effect / 100,
                ActionSpeed = 1 - CursesInfo.Forest.ActionSpeedMultiplier * curse.Effect / 100
            });
        }*/
    }

    private void Death()
    {
        OnDeath?.Invoke();
    }
}