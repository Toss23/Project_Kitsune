using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AbilityCaster))]
public abstract class UnitPresenter : MonoBehaviour, IUnitPresenter
{
    private static GameObject DamageIndication;
    private static GameObject DamageIndicationParent;

    [SerializeField] protected UnitInfo _info;

    // Base
    protected UnitType _unitType;
    protected Unit _unit;
    protected IUnitView _unitView;
    protected IAbilityCaster _abilityCaster;

    // Reference
    protected Rigidbody2D _rigidbody;
    protected ILogic _logic;

    public Transform Transform => transform;
    public UnitType UnitType => _unitType;
    public Unit Unit => _unit;
    public IUnitView UnitView => _unitView;

    public void Init(ILogic logic, UnitType unitType)
    {
        if (DamageIndication == null)
        {
            DamageIndication = Resources.Load<GameObject>("Damage");
            DamageIndicationParent = GameObject.FindWithTag("Damage Indication");
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _abilityCaster = GetComponent<AbilityCaster>();
        _logic = logic;

        _abilityCaster.Init(logic, this);

        _unitType = unitType;
        _unit = CreateUnit();
        _unitView = CreateUnitView();
        _unitView.CreateUnit(_info.Prefab, _unit.UnitInfo.AnimationAttackTime);

        Enable();
    }

    protected abstract void OnDisablePresenter();
    protected abstract void OnEnablePresenter();

    protected abstract IUnitView CreateUnitView();
    protected abstract Unit CreateUnit();

    public void Enable()
    {
        _logic.OnUpdate += _unit.Update;
        _logic.OnFixedUpdate += _unit.FixedUpdate;

        _unit.OnDeath += Death;
        _unit.Abilities.OnCastReloaded += _abilityCaster.CreateAbility;
        _unit.Abilities.OnLevelUpPassive += _unit.TryRemovePassiveAbility;
        _unit.Attributes.ActionSpeed.OnMultiplierChanged += _unitView.SetActionSpeed;
        _unit.Attributes.MagicShield.OnChanged += (value) => _unitView.SetMagicShield(value > 0);
        _unit.Curses.OnCursed += (curse) => _unitView.SetCurseIcon(curse, true);
        _unit.Curses.OnCurseCleared += (curse) => _unitView.SetCurseIcon(curse, false);
        if (_unit.Abilities.Levels[0] == 0) _unit.Abilities.LevelUp(0);

        _unit.OnDealDamage += ShowDealDamage;

        _unit.Abilities.OnChangeActive += _unitView.SetAttacking;

        OnEnablePresenter();
    }

    public void Disable()
    {
        _logic.OnUpdate -= _unit.Update;
        _logic.OnFixedUpdate -= _unit.FixedUpdate;

        _unit.OnDeath -= Death;
        _unit.Abilities.OnCastReloaded -= _abilityCaster.CreateAbility;
        _unit.Abilities.OnLevelUpPassive -= _unit.TryRemovePassiveAbility;
        _unit.Attributes.ActionSpeed.OnMultiplierChanged -= _unitView.SetActionSpeed;
        _unit.Attributes.MagicShield.OnChanged -= (value) => _unitView.SetMagicShield(value > 0);
        _unit.Curses.OnCursed -= (curse) => _unitView.SetCurseIcon(curse, true);
        _unit.Curses.OnCurseCleared -= (curse) => _unitView.SetCurseIcon(curse, false);

        _unit.OnDealDamage -= ShowDealDamage;

        _unit.Abilities.OnChangeActive -= _unitView.SetAttacking;

        OnDisablePresenter();
    }

    protected virtual void Death()
    {
        Disable();
        _unit.DisableAbilities();
        Destroy(gameObject);
    }

    private void ShowDealDamage(float damage, Unit target)
    {
        if (target != null)
        {
            Transform spawnTransform = target.UnitPresenter.Transform;
            GameObject damageIndication = Instantiate(DamageIndication, DamageIndicationParent.transform);
            damageIndication.name = "Damage <" + target + ">";
            damageIndication.transform.position = spawnTransform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
            damageIndication.GetComponent<DamageIndication>().Init(damage);
        }
    }
}
