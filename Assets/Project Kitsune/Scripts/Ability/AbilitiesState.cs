using System;

public enum Abilities
{
    Attack, First, Second, Third, Ultimate
}

public class AbilitiesState
{
    public event Action<IAbility, int> OnCastReloaded;

    private IAbility[] _abilities;
    private int[] _levels;
    private int[] _maxLevels;
    private float[] _reloadTimes;

    public AbilitiesState(IAbility[] abilities)
    {
        _abilities = abilities;
        _levels = new int[_abilities.Length];
        _levels[0] = 1;
        _reloadTimes = new float[_abilities.Length];
        _maxLevels = new int[_abilities.Length];
        for (int i = 0; i < _abilities.Length; i++)
        {
            if (_abilities[i] != null)
                _maxLevels[i] = _abilities[i].Damage.Length - 1;
        }
    }

    public void UpdateCastTime(float deltaTime)
    {
        for (int i = 0; i < _abilities.Length; i++)
        {
            if (_abilities[i] != null & _levels[i] > 0)
            {
                _reloadTimes[i] += deltaTime;
                float castPerSecond = _abilities[i].CastPerSecond[_levels[i]];
                while (_reloadTimes[i] >= 1 / castPerSecond)
                {
                    _reloadTimes[i] -= 1 / castPerSecond;
                    OnCastReloaded?.Invoke(_abilities[i], _levels[i]);
                }
            }
        }
    }

    public int LevelUp(Abilities type)
    {
        if (_levels[(int)type] < _maxLevels[(int)type])
            _levels[(int)type]++;
        return _levels[(int)type];
    }

    public int GetAbilityLevel(Abilities type)
    {
        return _levels[(int)type];
    }
}