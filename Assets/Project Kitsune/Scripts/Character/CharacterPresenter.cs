using UnityEngine;
using System;

[RequireComponent(typeof(CharacterView))]
public class CharacterPresenter : UnitPresenter
{
    public event Action<bool> OnFreeze;

    [SerializeField] private Joystick _joystick;
    [SerializeField] private ProgressBar _lifeBar;
    [SerializeField] private ProgressBar _magicShieldBar;
    [SerializeField] private ProgressBar _experienceBar;
    [SerializeField] private AbilitiesSelectionPresenter _abilitiesSelectionPresenter;

    protected override IUnit CreateUnit() => new Character(_info, GetComponent<Rigidbody2D>());
    protected override IUnitView CreateUnitView() => GetComponent<CharacterView>();
    protected override bool IsCharacter() => true;

    protected override void BeforeAwake()
    {
        
    }

    protected override void AfterAwake()
    {
        _abilitiesSelectionPresenter.Init(this);
        Enable();
    }

    protected override void OnEnablePresenter()
    {
        Controlable controlable = ((Character)_unit).Controlable;
        _joystick.OnActive += (angle, deltaTime) => controlable.Move(angle, deltaTime);
        _joystick.OnActive += (angle, deltaTime) => _unitView.SetAngle(angle);
        _joystick.IsActive += (active) => _unitView.IsMoving(active);

        Life life = _unit.Attributes.Life;
        _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());
        life.OnLifeChange += (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Life magicShield = _unit.Attributes.MagicShield;
        _magicShieldBar.SetPercentAndText(magicShield.GetPercent(), magicShield.ToString());
        magicShield.OnLifeChange += (value) => _magicShieldBar.SetPercentAndText(magicShield.GetPercent(), magicShield.ToString());

        Level level = _unit.Attributes.Level;
        _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged += (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());

        OnFreeze += controlable.Freeze;
        OnFreeze += _unit.Abilities.Freeze;
        OnFreeze += _unitView.Freeze;
        OnFreeze += _unit.Immune;

        AbilitiesSelection abilitiesSelection = _abilitiesSelectionPresenter.AbilitiesSelection;
        abilitiesSelection.OnAbilitiesListGenerated += (abilities, levels) => FreezeAll();
        abilitiesSelection.OnAbilityUpped += (ability) => UnfreezeAll();
        abilitiesSelection.OnAbilityUpCanceled += UnfreezeAll;
    }

    protected override void OnDisablePresenter()
    {
        Controlable controlable = ((Character)_unit).Controlable;
        _joystick.OnActive -= (angle, deltaTime) => controlable.Move(angle, deltaTime);
        _joystick.OnActive -= (angle, deltaTime) => _unitView.SetAngle(angle);
        _joystick.IsActive -= (active) => _unitView.IsMoving(active);

        Life life = _unit.Attributes.Life;
        life.OnLifeChange -= (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Life magicShield = _unit.Attributes.MagicShield;
        magicShield.OnLifeChange -= (value) => _magicShieldBar.SetPercentAndText(magicShield.GetPercent(), magicShield.ToString());

        Level level = _unit.Attributes.Level;
        level.OnExperienceChanged -= (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());

        OnFreeze -= controlable.Freeze;
        OnFreeze -= _unit.Abilities.Freeze;
        OnFreeze -= _unitView.Freeze;
        OnFreeze -= _unit.Immune;

        AbilitiesSelection abilitiesSelection = _abilitiesSelectionPresenter.AbilitiesSelection;
        abilitiesSelection.OnAbilitiesListGenerated -= (abilities, levels) => FreezeAll();
        abilitiesSelection.OnAbilityUpped -= (ability) => UnfreezeAll();
        abilitiesSelection.OnAbilityUpCanceled -= UnfreezeAll;
    }

    private void FreezeAll()
    {
        OnFreeze?.Invoke(true);
    }

    private void UnfreezeAll()
    {
        OnFreeze?.Invoke(false);
    }

    protected override void OnDeath()
    {

    }
}