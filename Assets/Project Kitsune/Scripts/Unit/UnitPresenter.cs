using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class UnitPresenter : MonoBehaviour, IUnitPresenter
{
    [SerializeField] protected UnitInfo _info;

    // Base
    protected UnitType _unitType;
    protected IUnit _unit;
    protected IUnitView _unitView;
    protected GameObject _unitViewObject;

    // Reference
    protected Rigidbody2D _rigidbody;
    private IGameLogic _gameLogic;

    public Transform Transform => transform;
    public IUnit Unit => _unit;
    public IUnitView UnitView => _unitView;

    public void Init(UnitType unitType)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gameLogic = GameLogic.Instance;

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
        _unit.Abilities.OnCastReloaded += CreateAbility;
        _unit.Abilities.OnLevelUpAttack += _unitView.SetAttackAnimationTime;
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
        _unit.Abilities.OnCastReloaded -= CreateAbility;
        _unit.Abilities.OnLevelUpAttack -= _unitView.SetAttackAnimationTime;
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
                abilityObject.name = ability.Info.Name;
                abilityObject.Init(level, (_unitType == UnitType.Character) ? Target.Enemy : Target.Character);
                Transform abilityTransform = abilityObject.gameObject.transform;
                abilityTransform.position = _unitView.AbilityPoints.Points[point].transform.position;
                if (ability.Info.AbilityType != AbilityInfo.Type.Field)
                    abilityTransform.Rotate(new Vector3(0, 0, _unitView.Angle + startAngle + deltaAngle * i));

                if (ability.Info.AbilityType == AbilityInfo.Type.Melee
                    || ability.Info.AbilityType == AbilityInfo.Type.Field)
                    abilityObject.FuseWith(_unitView.AbilityPoints.Points[point].transform);

                _unit.RegisterAbility(abilityObject);
            }

            if (ability.Info.HaveAura)
            {
                GameObject auraObject = Instantiate(ability.Info.AuraObject);
                auraObject.transform.parent = _unitView.AbilityPoints.PointsAura[point].transform;
                auraObject.transform.position = _unitView.AbilityPoints.PointsAura[point].transform.position;
                auraObject.transform.localScale = ability.Info.AuraObject.transform.localScale;
            }
        }
    }
}
