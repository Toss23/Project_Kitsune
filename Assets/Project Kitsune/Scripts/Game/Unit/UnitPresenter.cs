using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AbilityCaster))]
public abstract class UnitPresenter : MonoBehaviour, IUnitPresenter
{
    [SerializeField] protected UnitInfo _info;

    // Base
    protected UnitType _unitType;
    protected Unit _unit;
    protected IUnitView _unitView;
    protected IAbilityCaster _abilityCaster;

    // Reference
    protected Rigidbody2D _rigidbody;
    protected IContext _context;

    public Transform Transform => transform;
    public UnitType UnitType => _unitType;
    public Unit Unit => _unit;
    public IUnitView UnitView => _unitView;

    public void Init(IContext context, UnitType unitType)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _abilityCaster = GetComponent<AbilityCaster>();
        _context = context;

        _abilityCaster.Init(context, this);

        _unitType = unitType;
        _unit = CreateUnit();
        _unitView = CreateUnitView();
        _unitView.CreateUnit(context, _info.Prefab, _unit.UnitInfo.AnimationAttackTime);

        Enable();
    }

    protected abstract void OnDisablePresenter();
    protected abstract void OnEnablePresenter();

    protected abstract IUnitView CreateUnitView();
    protected abstract Unit CreateUnit();

    public void Enable()
    {
        _context.OnUpdate += _unit.Update;
        _context.OnFixedUpdate += _unit.FixedUpdate;

        _unit.OnDeath += Death;
        _unit.AbilitiesContainer.OnCastReloaded += _abilityCaster.CreateAbility;
        _unit.AbilitiesContainer.OnLevelUpPassive += _unit.TryRemovePassiveAbility;
        _unit.AttributesContainer.ActionSpeed.OnMultiplierChanged += _unitView.SetActionSpeed;
        _unitView.SetMagicShield(_unit.AttributesContainer.MagicShield.Value > 0);
        _unit.AttributesContainer.MagicShield.OnChanged += (value) => _unitView.SetMagicShield(value > 0);
        _unit.CursesContainer.OnCursed += (curse) => _unitView.SetCurseIcon(curse, true);
        _unit.CursesContainer.OnCurseCleared += (curse) => _unitView.SetCurseIcon(curse, false);
        if (_unit.AbilitiesContainer.Levels[0] == 0) _unit.AbilitiesContainer.LevelUp(0);

        _unit.OnDealDamage += ShowDealDamage;

        _unit.AbilitiesContainer.OnChangeActive += _unitView.SetAttacking;

        OnEnablePresenter();
    }

    public void Disable()
    {
        _context.OnUpdate -= _unit.Update;
        _context.OnFixedUpdate -= _unit.FixedUpdate;

        _unit.OnDeath -= Death;
        _unit.AbilitiesContainer.OnCastReloaded -= _abilityCaster.CreateAbility;
        _unit.AbilitiesContainer.OnLevelUpPassive -= _unit.TryRemovePassiveAbility;
        _unit.AttributesContainer.ActionSpeed.OnMultiplierChanged -= _unitView.SetActionSpeed;
        _unit.AttributesContainer.MagicShield.OnChanged -= (value) => _unitView.SetMagicShield(value > 0);
        _unit.CursesContainer.OnCursed -= (curse) => _unitView.SetCurseIcon(curse, true);
        _unit.CursesContainer.OnCurseCleared -= (curse) => _unitView.SetCurseIcon(curse, false);

        _unit.OnDealDamage -= ShowDealDamage;

        _unit.AbilitiesContainer.OnChangeActive -= _unitView.SetAttacking;

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
        if (target != null & _context.DamageIndication != null & _context.DamageIndicationParent != null)
        {
            Transform spawnTransform = target.UnitPresenter.Transform;
            GameObject damageIndication = Instantiate(_context.DamageIndication, _context.DamageIndicationParent.transform);
            damageIndication.name = "Damage <" + target + ">";
            damageIndication.transform.position = spawnTransform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
            damageIndication.GetComponent<DamageIndication>().Init(damage);
        }
    }
}
