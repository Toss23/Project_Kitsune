using System;

public class AbilitiesState
{
    public event Action<IAbility, AbilityType, int> OnCastReloaded;

    private IAbility[] _abilities;
    private int[] _levels;
    private int[] _maxLevels;
    private float[] _reloadTimes;
    private bool[] _casted;

    public IAbility[] List => _abilities;

    public AbilitiesState(IAbility[] abilities)
    {
        _abilities = abilities;
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
                    if (_abilities[i].Info.AbilityType != AbilityInfo.Type.Field ||
                        (_abilities[i].Info.AbilityType == AbilityInfo.Type.Field && _casted[i] == false))
                    {
                        _reloadTimes[i] += deltaTime;
                        float castPerSecond = _abilities[i].Info.CastPerSecond[_levels[i]];
                        while (_reloadTimes[i] >= 1 / castPerSecond)
                        {
                            _reloadTimes[i] -= 1 / castPerSecond;
                            _casted[i] = true;
                            OnCastReloaded?.Invoke(_abilities[i], (AbilityType)i, _levels[i]);
                        }
                    }
                }
            }
        }
    }

    public void LevelUp(AbilityType type)
    {
        _levels[(int)type]++;
        if (_levels[(int)type] > _maxLevels[(int)type])
            _levels[(int)type] = _maxLevels[(int)type];
    }
}