using UnityEngine;
using System;

[RequireComponent(typeof(CharacterView))]
public class CharacterPresenter : UnitPresenter
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ProgressBar _lifeBar;
    [SerializeField] private ProgressBar _magicShieldBar;
    [SerializeField] private ProgressBar _experienceBar;
    [SerializeField] private AbilitiesSelectionPresenter _abilitiesSelectionPresenter;

    public Character UnitCharacter => (Character)_unit;

    protected override IUnit CreateUnit() => new Character(_info, GetComponent<Rigidbody2D>());
    protected override IUnitView CreateUnitView() => GetComponent<CharacterView>();

    protected override void OnEnablePresenter()
    {
        Controlable controlable = ((Character)_unit).Controlable;
        _joystick.OnActive += (angle, deltaTime) => controlable.Move(angle, deltaTime);
        _joystick.OnActive += (angle, deltaTime) => _unitView.SetAngle(angle);
        _joystick.IsActive += (active) => _unitView.IsMoving(active);

        Life life = _unit.Attributes.Life;
        _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());
        life.OnChanged += (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Life magicShield = _unit.Attributes.MagicShield;
        _magicShieldBar.SetPercentAndText(magicShield.GetPercent(), magicShield.ToString());
        magicShield.OnChanged += (value) => _magicShieldBar.SetPercentAndText(magicShield.GetPercent(), magicShield.ToString());

        Level level = _unit.Attributes.Level;
        _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged += (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());

        
        /*
        AbilitiesSelection abilitiesSelection = _abilitiesSelectionPresenter.AbilitiesSelection;
        abilitiesSelection.OnAbilitiesListGenerated += (abilities, levels) => FreezeAll();
        abilitiesSelection.OnAbilityUpped += (ability) => UnfreezeAll();
        abilitiesSelection.OnAbilityUpCanceled += UnfreezeAll;
        */  
    }

    protected override void OnDisablePresenter()
    {
        Controlable controlable = ((Character)_unit).Controlable;
        _joystick.OnActive -= (angle, deltaTime) => controlable.Move(angle, deltaTime);
        _joystick.OnActive -= (angle, deltaTime) => _unitView.SetAngle(angle);
        _joystick.IsActive -= (active) => _unitView.IsMoving(active);

        Life life = _unit.Attributes.Life;
        life.OnChanged -= (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Life magicShield = _unit.Attributes.MagicShield;
        magicShield.OnChanged -= (value) => _magicShieldBar.SetPercentAndText(magicShield.GetPercent(), magicShield.ToString());

        Level level = _unit.Attributes.Level;
        level.OnExperienceChanged -= (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
        
        
        /*
        AbilitiesSelection abilitiesSelection = _abilitiesSelectionPresenter.AbilitiesSelection;
        abilitiesSelection.OnAbilitiesListGenerated -= (abilities, levels) => FreezeAll();
        abilitiesSelection.OnAbilityUpped -= (ability) => UnfreezeAll();
        abilitiesSelection.OnAbilityUpCanceled -= UnfreezeAll;
        */
    }

    protected override void OnDeath()
    {
        GameLogic.Instance.EndGame();
    }
}