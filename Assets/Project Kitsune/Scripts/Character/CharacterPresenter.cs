using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class CharacterPresenter : UnitPresenter
{
    public Character Character => (Character)_unit;

    protected override Unit CreateUnit() => new Character(_info, this, GetComponent<Rigidbody2D>());
    protected override IUnitView CreateUnitView() => GetComponent<CharacterView>();

    protected override void OnEnablePresenter()
    {
        ICharacterView characterView = (ICharacterView)UnitView;

        Controllable controllable = Character.Controllable;
        characterView.Joystick.OnMoveStick += controllable.Move;
        characterView.Joystick.OnMoveStick += _unitView.SetMovingAndAngle;
        characterView.Joystick.OnActiveChanged += _unit.Abilities.CancelAttack;

        Life life = _unit.Attributes.Life;
        Life magicShield = _unit.Attributes.MagicShield;
        UpdateLifeBar(characterView, life, magicShield);
        characterView.MagicShieldBar?.SetPercentAndText(magicShield.GetPercent(), "");
        life.OnChanged += (value) => UpdateLifeBar(characterView, life, magicShield);
        magicShield.OnChanged += (value) => characterView.MagicShieldBar?.SetPercentAndText(magicShield.GetPercent(), "");

        Level level = _unit.Attributes.Level;
        characterView.ExperienceBar?.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged += (value) => characterView.ExperienceBar?.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnLevelUp += (value) => characterView.ExperienceBar?.SetPercentAndText(level.GetPercent(), level.ToString());
    }

    protected override void OnDisablePresenter()
    {
        ICharacterView characterView = (ICharacterView)UnitView;

        Controllable controllable = Character.Controllable;
        characterView.Joystick.OnMoveStick -= controllable.Move;
        characterView.Joystick.OnMoveStick -= _unitView.SetMovingAndAngle;
        characterView.Joystick.OnActiveChanged -= _unit.Abilities.CancelAttack;

        Life life = _unit.Attributes.Life;
        Life magicShield = _unit.Attributes.MagicShield;
        UpdateLifeBar(characterView, life, magicShield);
        characterView.MagicShieldBar?.SetPercentAndText(magicShield.GetPercent(), "");
        life.OnChanged -= (value) => UpdateLifeBar(characterView, life, magicShield);
        magicShield.OnChanged -= (value) => characterView.MagicShieldBar?.SetPercentAndText(magicShield.GetPercent(), "");

        Level level = _unit.Attributes.Level;
        characterView.ExperienceBar?.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged -= (value) => characterView.ExperienceBar?.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnLevelUp -= (value) => characterView.ExperienceBar?.SetPercentAndText(level.GetPercent(), level.ToString());
    }

    protected override void Death()
    {
        _unit.DisableAbilities();
        _logic.EndGame();
    }

    private void UpdateLifeBar(ICharacterView characterView, Life life, Life magicShield)
    {
        if (magicShield.Maximum > 0)
        {
            characterView.LifeBar?.SetPercentAndText(life.GetPercent(), life.ToString() + " (" + magicShield.ToString() + ")");
        }
        else
        {
            characterView.LifeBar?.SetPercentAndText(life.GetPercent(), life.ToString());
        }
    }
}