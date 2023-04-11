using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class CharacterPresenter : UnitPresenter
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ProgressBar _lifeBar;
    [SerializeField] private ProgressBar _experienceBar;

    protected override IUnit CreateUnit() => new Character(_info);
    protected override IUnitView CreateUnitView() => GetComponent<CharacterView>();
    protected override bool IsCharacter() => true;
    
    protected override void OnAwake()
    {
        Enable();
    }

    protected override void OnEnablePresenter()
    {
        Controlable controlable = ((Character)_unit).Controlable;
        _joystick.OnActive += (angle, deltaTime) => controlable.Move(angle, deltaTime);
        _joystick.OnActive += (angle, deltaTime) => _unitView.SetAngle(angle);
        _joystick.IsActive += (active) => _unitView.IsMoving(active);
        controlable.OnMove += (position) => _unitView.SetPosition(position);

        Life life = _unit.Attributes.Life;
        _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());
        life.OnLifeChange += (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Level level = _unit.Attributes.Level;
        _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged += (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
    }

    protected override void OnDisablePresenter()
    {
        Controlable controlable = ((Character)_unit).Controlable;
        _joystick.OnActive -= (angle, deltaTime) => controlable.Move(angle, deltaTime);
        _joystick.OnActive -= (angle, deltaTime) => _unitView.SetAngle(angle);
        _joystick.IsActive -= (active) => _unitView.IsMoving(active);
        controlable.OnMove -= (position) => _unitView.SetPosition(position);

        Life life = _unit.Attributes.Life;
        _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());
        life.OnLifeChange -= (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Level level = _unit.Attributes.Level;
        _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged -= (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
    }
}