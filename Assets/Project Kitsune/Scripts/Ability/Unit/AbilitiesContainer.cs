using System;

public class AbilitiesContainer
{
    /// <summary>
    /// IAbility - ability for cast <br/>
    /// int - ability point <br/>
    /// int - ability level <br/>
    /// </summary>
    public event Action<bool> OnChangeActive;
    public event Action<IAbility, int, int> OnCastReloaded;
    public event Action<IAbility> OnLevelUpPassive;

    private Unit _unit;
    private IAbility[] _abilities;
    private float _animationTimeToAttack;
    private int[] _levels;
    private int[] _maxLevels;
    private float[] _reloadTimes;
    private bool[] _casted;
    private AbilityModifier[] _abilityModifiers;
    private bool _active;

    public IAbility[] List => _abilities;
    public int[] Levels => _levels;
    public int[] MaxLevels => _maxLevels;

    public AbilitiesContainer(Unit unit, IAbility[] abilities, AbilityModifier[] abilityModifiers)
    {
        _unit = unit;
        _abilities = abilities;
        _animationTimeToAttack = unit.UnitInfo.AnimationTimeToAttack;
        _levels = new int[_abilities.Length];
        _maxLevels = new int[_abilities.Length];
        _reloadTimes = new float[_abilities.Length];
        _casted = new bool[_abilities.Length];
        _abilityModifiers = abilityModifiers;

        for (int i = 0; i < _abilities.Length; i++)
        {
            if (_abilities[i] != null)
                _maxLevels[i] = _abilities[i].AbilityData.GetMaxLevel();
        }

        SetActive(true);
    }

    public void SetActive(bool active)
    {
        _active = active;
        OnChangeActive?.Invoke(active);
    }

    public void Update(float deltaTime)
    {
        if (_active == true)
        {
            float actionSpeed = _unit.Attributes.ActionSpeed.Value;
            deltaTime *= actionSpeed;

            for (int i = 0; i < _abilities.Length; i++)
            {
                if (_abilities[i] != null & _levels[i] > 0)
                {
                    AbilityData.Type type = _abilities[i].AbilityData.GetAbilityType();
                    if (type != AbilityData.Type.NonDamage & type != AbilityData.Type.Passive)
                    {
                        BaseAbilityData baseAbilityData = (BaseAbilityData)_abilities[i].AbilityData;
                        _reloadTimes[i] += deltaTime;
                        float castPerSecond = baseAbilityData.CastPerSecond.Get(_levels[i]) + _abilityModifiers[i].CastPerSecond;
                        float attackTime = 1 / castPerSecond;

                        if (i == 0)
                        {
                            attackTime *= _animationTimeToAttack;
                        }

                        while (_reloadTimes[i] >= attackTime)
                        {
                            _reloadTimes[i] -= 1 / castPerSecond;
                            OnCastReloaded?.Invoke(_abilities[i], i, _levels[i]);
                        }
                    }
                    else if (_casted[i] == false)
                    {
                        OnCastReloaded?.Invoke(_abilities[i], i, _levels[i]);
                        _casted[i] = true;
                    }
                }
            }
        }
    }

    public void CancelAttack()
    {
        _casted[0] = false;
        _reloadTimes[0] = 0;
    }

    public void LevelUp(int index)
    {
        if (_abilities[index] != null)
        {
            _levels[index]++;
            if (_levels[index] > _maxLevels[index])
                _levels[index] = _maxLevels[index];

            if (index == 0)
            {
                CancelAttack();
            }

            AbilityData.Type type = _abilities[index].AbilityData.GetAbilityType();
            if (type == AbilityData.Type.NonDamage | type == AbilityData.Type.Passive)
            {
                OnLevelUpPassive?.Invoke(_abilities[index]);
                _casted[index] = false;
            }
        }
    }

    public void LevelUp(IAbility ability)
    {
        if (ability != null)
        {
            for (int i = 0; i < _abilities.Length; i++)
            {
                if (ability.AbilityData.Name == _abilities[i].AbilityData.Name)
                {
                    LevelUp(i);
                    break;
                }
            }
        }
    }
}