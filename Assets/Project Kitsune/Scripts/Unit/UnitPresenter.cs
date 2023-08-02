using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AbilityCaster))]
public abstract class UnitPresenter : MonoBehaviour, IUnitPresenter
{
    [SerializeField] protected UnitInfo _info;

    // Base
    protected UnitType _unitType;
    protected IUnit _unit;
    protected IUnitView _unitView;
    protected IAbilityCaster _abilityCaster;

    // Reference
    protected Rigidbody2D _rigidbody;
    private IGameLogic _gameLogic;

    public Transform Transform => transform;
    public UnitType UnitType => _unitType;
    public IUnit Unit => _unit;
    public IUnitView UnitView => _unitView;

    public void Init(UnitType unitType)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _abilityCaster = GetComponent<AbilityCaster>();
        _gameLogic = GameLogic.Instance;

        _abilityCaster.Init(this);

        _unitType = unitType;
        _unit = CreateUnit();
        _unitView = CreateUnitView();
        _unitView.CreateUnit(_info.Prefab);

        Enable();
    }

    protected abstract void OnDeath();

    protected abstract void OnDisablePresenter();
    protected abstract void OnEnablePresenter();

    protected abstract IUnitView CreateUnitView();
    protected abstract IUnit CreateUnit();

    public void Enable()
    {
        _gameLogic.OnUpdate += _unit.Update;
        _gameLogic.OnFixedUpdate += _unit.FixedUpdate;

        _unit.OnDeath += Death;
        _unit.Abilities.OnCastReloaded += _abilityCaster.CreateAbility;
        _unit.Abilities.OnLevelUpAttack += _unitView.SetAnimationAttackSpeed;
        _unit.Abilities.OnLevelUpField += _unit.TryRemoveField;
        _unit.Attributes.MagicShield.OnChanged += (value) => _unitView.SetMagicShield(value > 0);
        _unit.Curses.OnCursed += (curse) => _unitView.SetCurseIcon(curse, true);
        _unit.Curses.OnCurseCleared += (curse) => _unitView.SetCurseIcon(curse, false);
        if (_unit.Abilities.Levels[0] == 0) _unit.Abilities.LevelUp(0);
        OnEnablePresenter();
    }

    public void Disable()
    {
        _gameLogic.OnUpdate -= _unit.Update;
        _gameLogic.OnFixedUpdate -= _unit.FixedUpdate;

        _unit.OnDeath -= Death;
        _unit.Abilities.OnCastReloaded -= _abilityCaster.CreateAbility;
        _unit.Abilities.OnLevelUpAttack -= _unitView.SetAnimationAttackSpeed;
        _unit.Abilities.OnLevelUpField -= _unit.TryRemoveField;
        _unit.Attributes.MagicShield.OnChanged -= (value) => _unitView.SetMagicShield(value > 0);
        _unit.Curses.OnCursed -= (curse) => _unitView.SetCurseIcon(curse, true);
        _unit.Curses.OnCurseCleared -= (curse) => _unitView.SetCurseIcon(curse, false);
        OnDisablePresenter();
    }

    private void Death()
    {
        OnDeath();
        Disable();
        _unit.DisableAbilities();
        Destroy(gameObject);
    }
}
