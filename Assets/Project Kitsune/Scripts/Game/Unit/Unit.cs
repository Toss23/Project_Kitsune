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

    public bool IsImmune => _isImmune;
    public AttributesContainer AttributesContainer { get; private set; }
    public AbilitiesContainer AbilitiesContainer { get; private set; }
    public CursesContainer CursesContainer { get; private set; }
    public ModifiersContainer ModifiersContainer { get; private set; }
    public UnitInfo UnitInfo => _unitInfo;
    public IUnitPresenter UnitPresenter => _unitPresenter;

    protected void Init(UnitInfo unitInfo, IUnitPresenter unitPresenter)
    {
        _unitInfo = unitInfo;
        _unitPresenter = unitPresenter;
        _castedAbilities = new List<IAbility>();

        AttributesContainer = new AttributesContainer(this);
        AttributesContainer.Life.OnMinimum += Death;
        AttributesContainer.Level.OnLevelUp += LevelUp;

        ModifiersContainer = new ModifiersContainer(AttributesContainer);

        AbilitiesContainer = new AbilitiesContainer(this, unitInfo.Abilities, ModifiersContainer.AbilityModifiers);

        CursesContainer = new CursesContainer();
        CursesContainer.OnCursed += OnCursed;
        CursesContainer.OnCurseCleared += OnCurseCleared;
    }

    public void Update(float deltaTime)
    {
        AttributesContainer.Update(deltaTime);
        AbilitiesContainer.Update(deltaTime);
        CursesContainer.Update(deltaTime);
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
        if (CursesContainer.Have(Curses.List.Weakness))
        {
            Curse weakness = CursesContainer.Find(Curses.List.Weakness);
            value *= (1 + weakness.Effect / 100f) * Curses.Weakness.InputDamageMultiplier;
        }

        value *= (1 - _unitInfo.Armour / 100f);

        if (_isImmune == false)
        {
            if (isProjectile)
            {
                AttributesContainer.Life.TakeDamage(value);
            }
            else
            {
                float excess = AttributesContainer.MagicShield.TakeDamage(value);
                AttributesContainer.Life.TakeDamage(excess);
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
            float damage = Damage.CalculateAbilityDamage(AttributesContainer.Damage, ability, ability.Level);
            bool isProjectile = ability.AbilityData.GetAbilityType() == AbilityData.Type.Range;

            if (CursesContainer.Have(Curses.List.Weakness))
            {
                Curse weakness = CursesContainer.Find(Curses.List.Weakness);
                damage *= weakness.Effect / 100f * Curses.Weakness.OutputDamageMultiplier;
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
        if (curse.Name == Curses.List.Forest)
        {
            ModifiersContainer.Add(new AttributeModifier()
            {
                Movespeed = 1 - Curses.Forest.ActionSpeedMultiplier * curse.Effect / 100,
                ActionSpeed = 1 - Curses.Forest.ActionSpeedMultiplier * curse.Effect / 100
            });
        }
    }

    private void OnCurseCleared(Curse curse)
    {
        if (curse.Name == Curses.List.Forest)
        {
            ModifiersContainer.Remove(new AttributeModifier()
            {
                Movespeed = 1 - Curses.Forest.ActionSpeedMultiplier * curse.Effect / 100,
                ActionSpeed = 1 - Curses.Forest.ActionSpeedMultiplier * curse.Effect / 100
            });
        }
    }

    private void Death()
    {
        OnDeath?.Invoke();
    }
}