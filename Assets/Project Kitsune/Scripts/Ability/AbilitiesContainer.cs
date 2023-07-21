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
    /// float - attack cast speed
    /// </summary>
    public event Action<float> OnLevelUpAttack;

    private IAbility[] _abilities;
    private float _attackAnimationTime;
    private int[] _levels;
    private int[] _maxLevels;
    private float[] _reloadTimes;
    private bool[] _casted;

    public IAbility[] List => _abilities;
    public int[] Levels => _levels;
    public int[] MaxLevels => _maxLevels;

    public AbilitiesContainer(IAbility[] abilities, float attackAnimationTime)
    {
        _abilities = abilities;
        _attackAnimationTime = attackAnimationTime;
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
            if (_abilities[i] != null)
            {
                if (_levels[i] > 0)
                {
                    if (_abilities[i].Info.AbilityType != AbilityInfo.Type.Field)                           
                    {
                        _reloadTimes[i] += deltaTime;
                        float castPerSecond = _abilities[i].Info.CastPerSecond[_levels[i]];
                        while (_reloadTimes[i] >= 1 / castPerSecond)
                        {
                            _reloadTimes[i] -= 1 / castPerSecond;
                            _casted[i] = true;
                            OnCastReloaded?.Invoke(_abilities[i], i, _levels[i]);
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
    }

    public void LevelUp(int type)
    {
        _levels[type]++;
        if (_levels[type] > _maxLevels[type])
            _levels[type] = _maxLevels[type];

        if (type == 0 & _abilities[0] != null)
        {
            float multiplier = _attackAnimationTime * _abilities[0].Info.CastPerSecond[_levels[0]];
            OnLevelUpAttack?.Invoke(multiplier);
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
                    _levels[i]++;
                    if (_levels[i] > _maxLevels[i])
                        _levels[i] = _maxLevels[i];

                    if (i == 0)
                    {
                        float multiplier = _attackAnimationTime * _abilities[0].Info.CastPerSecond[_levels[0]];
                        OnLevelUpAttack?.Invoke(multiplier);
                    }
                    break;
                }
            }
        }
    }
}