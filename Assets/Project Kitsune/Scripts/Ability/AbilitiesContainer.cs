using System;

public class AbilitiesContainer
{
    /// <summary>
    /// IAbility - ability for cast <br/>
    /// int - ability point <br/>
    /// int - ability level <br/>
    /// </summary>
    public event Action<IAbility, int, int> OnCastReloaded;
    /// <summary>
    /// float - attack speed
    /// </summary>
    public event Action<float> OnLevelUpAttack;
    public event Action<IAbility> OnLevelUpField;

    private IAbility[] _abilities;
    private float _animationAttackSpeed;
    private float _animationTimeToAttack;
    private int[] _levels;
    private int[] _maxLevels;
    private float[] _reloadTimes;
    private bool[] _casted;

    public IAbility[] List => _abilities;
    public int[] Levels => _levels;
    public int[] MaxLevels => _maxLevels;

    public AbilitiesContainer(IAbility[] abilities, UnitInfo unitInfo)
    {
        _abilities = abilities;
        _animationAttackSpeed = unitInfo.AnimationAttackSpeed;
        _animationTimeToAttack = unitInfo.AnimationTimeToAttack;
        _levels = new int[_abilities.Length];
        _maxLevels = new int[_abilities.Length];
        _reloadTimes = new float[_abilities.Length];
        _casted = new bool[_abilities.Length];

        for (int i = 0; i < _abilities.Length; i++)
        {
            if (_abilities[i] != null)
                _maxLevels[i] = _abilities[i].MaxLevel;
        }
    }

    public void UpdateCastTime(float deltaTime)
    {
        
        for (int i = 0; i < _abilities.Length; i++)
        {
            if (_abilities[i] != null & _levels[i] > 0)
            {
                if (_abilities[i].Info.AbilityType != AbilityInfo.Type.Field)                           
                {
                    _reloadTimes[i] += deltaTime;
                    float castPerSecond = _abilities[i].Info.CastPerSecond[_levels[i]];

                    if (i != 0)
                    {
                        while (_reloadTimes[i] >= 1 / castPerSecond)
                        {
                            _reloadTimes[i] -= 1 / castPerSecond;
                            OnCastReloaded?.Invoke(_abilities[i], i, _levels[i]);
                        }
                    }
                    else
                    {
                        float attackSpeed = 1 / castPerSecond * _animationTimeToAttack;

                        if (_reloadTimes[i] >= attackSpeed & _casted[i] == false)
                        {
                            _casted[i] = true;
                            OnCastReloaded?.Invoke(_abilities[i], i, _levels[i]);
                        }

                        while (_reloadTimes[i] >= 1 / castPerSecond)
                        {
                            _casted[i] = false;
                            _reloadTimes[i] -= 1 / castPerSecond;
                        }
                    }
                }
                else if (_casted[i] == false)
                {
                    _casted[i] = true;
                    OnCastReloaded?.Invoke(_abilities[i], i, _levels[i]);
                }
            }
        }
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
                _casted[0] = false;
                _reloadTimes[0] = 0;
                float attackSpeed = _animationAttackSpeed * _abilities[0].Info.CastPerSecond[_levels[0]];
                OnLevelUpAttack?.Invoke(attackSpeed);
            }

            if (_abilities[index].Info.AbilityType == AbilityInfo.Type.Field)
            {
                _casted[index] = false;
                OnLevelUpField?.Invoke(_abilities[index]);
            }
        }
    }

    public void LevelUp(IAbility ability)
    {
        if (ability != null)
        {
            for (int i = 0; i < _abilities.Length; i++)
            {
                if (ability == _abilities[i])
                {
                    LevelUp(i);
                    break;
                }
            }
        }
    }
}