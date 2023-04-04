using System;

public enum Abilities
{
    Attack, First, Second, Third, Ultimate
}

public class AbilitiesState
{
    public event Action<Ability, int> OnCastReloaded;

    private Ability[] _abilities;
    private int[] _levels;
    private int[] _maxLevels;
    private float[] _reloadTimes;

    public AbilitiesState(Ability[] abilities)
    {
        _abilities = abilities;
        _levels = new int[abilities.Length];
        _levels[0] = 1;
        _reloadTimes = new float[abilities.Length];
        _maxLevels = new int[abilities.Length];
        for (int i = 0; i < abilities.Length; i++)
            _maxLevels[i] = abilities[i].Damage.Length;
    }

    public void UpdateCastTime(float deltaTime)
    {
        for (int i = 0; i < _reloadTimes.Length; i++)
        {
            if (_levels[i] > 0)
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