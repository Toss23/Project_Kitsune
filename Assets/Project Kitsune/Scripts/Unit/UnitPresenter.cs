using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class UnitPresenter : MonoBehaviour, IUnitPresenter
{
    [SerializeField] protected UnitInfo _info;

    protected IUnit _unit;
    protected IUnitView _unitView;
    protected Rigidbody2D _rigidbody;

    private bool _isEnable = false;
    private bool _isCharacter;

    public Transform Transform => transform;
    public IUnit Unit => _unit;
    public IUnitView UnitView => _unitView;

    private void Awake()
    {
        BeforeAwake();

        _rigidbody = GetComponent<Rigidbody2D>();
        _unit = CreateUnit();
        _unitView = CreateUnitView();
        _unitView.CreateUnit(_info.Prefab);
        _isCharacter = IsCharacter();

        AfterAwake();
    }

    protected abstract void OnDeath();

    protected abstract void OnDisablePresenter();
    protected abstract void OnEnablePresenter();

    protected abstract IUnitView CreateUnitView();
    protected abstract IUnit CreateUnit();
    protected abstract bool IsCharacter();

    protected abstract void BeforeAwake();
    protected abstract void AfterAwake();

    public void Enable()
    {
        _isEnable = true;
        _unit.OnDeath += Death;
        _unit.Abilities.OnCastReloaded += CreateAbility;
        _unit.Abilities.OnLevelUpAttack += _unitView.SetAttackAnimationTime;
        if (_unit.Abilities.Levels[0] == 0) _unit.Abilities.LevelUp(0);
        OnEnablePresenter();
    }

    public void Disable()
    {
        _isEnable = false;
        _unit.OnDeath -= Death;
        _unit.Abilities.OnCastReloaded -= CreateAbility;
        _unit.Abilities.OnLevelUpAttack -= _unitView.SetAttackAnimationTime;
        OnDisablePresenter();
    }

    private void Update()
    {
        if (_isEnable)
        {
            _unit.Update(Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (_isEnable)
        {
            _unit.FixedUpdate(Time.fixedDeltaTime);
        }
    }

    private void Death()
    {
        OnDeath();
        Disable();
        _unit.DisableAbilities();
        Destroy(gameObject);
    }

    protected void CreateAbility(IAbility ability, int point, int level)
    {
        if (ability != null)
        {
            int count = 1;
            float deltaAngle = 0;
            float startAngle = 0;

            if (ability.Info.AbilityType == AbilityInfo.Type.Projectile)
            {
                count = ability.Info.ProjectileCount[level];
                deltaAngle = ability.Info.ProjectileSpliteAngle[level];
                startAngle = -count * deltaAngle / 2f;
            }

            for (int i = 0; i < count; i++)
            {
                Ability abilityObject = Instantiate((Ability)ability);
                abilityObject.Init(level, _isCharacter ? Target.Enemy : Target.Character);
                Transform abilityTransform = abilityObject.gameObject.transform;
                abilityTransform.position = _unitView.AbilityPoints.Points[point].transform.position;
                if (ability.Info.AbilityType != AbilityInfo.Type.Field)
                    abilityTransform.Rotate(new Vector3(0, 0, _unitView.Angle + startAngle + deltaAngle * i));

                if (ability.Info.AbilityType == AbilityInfo.Type.Melee
                    || ability.Info.AbilityType == AbilityInfo.Type.Field)
                    abilityObject.FuseWith(_unitView.AbilityPoints.Points[point].transform);

                _unit.RegisterAbility(abilityObject);
            }
        }
    }
}
