using UnityEngine;

public abstract class UnitPresenter : MonoBehaviour, IUnitPresenter
{
    [SerializeField] protected UnitInfo _info;

    protected IUnit _unit;
    protected IUnitView _unitView;

    private bool _isEnable = false;
    private bool _isCharacter;

    public IUnit Unit => _unit;
    public Transform Transform => transform;

    private void Awake()
    {
        _unit = CreateUnit();
        _unitView = CreateUnitView();
        _unitView.CreateUnit(_info.Prefab);
        _isCharacter = IsCharacter();
        OnAwake();
    }

    protected abstract IUnit CreateUnit();
    protected abstract IUnitView CreateUnitView();
    protected abstract bool IsCharacter();
    protected abstract void OnAwake();

    public void Enable()
    {
        _isEnable = true;
        OnEnablePresenter();
        _unit.Abilities.OnCastReloaded += CreateAbility;       
    }

    public void Disable()
    {
        _isEnable = false;
        OnDisablePresenter();
        _unit.Abilities.OnCastReloaded -= CreateAbility;      
    }

    protected abstract void OnEnablePresenter();
    protected abstract void OnDisablePresenter();

    private void Update()
    {
        if (_isEnable)
        {
            _unit.Update(Time.deltaTime);
        }
    }

    protected void CreateAbility(IAbility ability, AbilityType type, int level)
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
                abilityObject.Init(level, _isCharacter);
                Transform abilityTransform = abilityObject.gameObject.transform;
                abilityTransform.position = _unitView.AbilityPoints.Points[(int)type].transform.position;
                abilityTransform.Rotate(new Vector3(0, 0, _unitView.Angle + startAngle + deltaAngle * i));

                if (ability.Info.AbilityType == AbilityInfo.Type.Melee
                    || ability.Info.AbilityType == AbilityInfo.Type.Field)
                    abilityObject.FuseWith(_unitView.AbilityPoints.Points[(int)type].transform);

                _unit.RegisterAbility(abilityObject);
            }
        }
    }
}
