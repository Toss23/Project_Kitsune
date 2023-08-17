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
    public event Action<IAbility> OnLevelUpPassive;

    private Unit _unit;
    private IAbility[] _abilities;
    private float _animationAttackSpeed;
    private float _animationTimeToAttack;
    private int[] _levels;
    private int[] _maxLevels;
    private float[] _reloadTimes;
    private bool[] _casted;
    private AbilityModifier[] _abilityModifiers;

    public IAbility[] List => _abilities;
    public int[] Levels => _levels;
    public int[] MaxLevels => _maxLevels;

    public AbilitiesContainer(Unit unit, IAbility[] abilities, AbilityModifier[] abilityModifiers)
    {
        _unit = unit;
        _abilities = abilities;
        _animationAttackSpeed = unit.UnitInfo.AnimationAttackTime;
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
    }

    public void Update(float deltaTime)
    {       
        for (int i = 0; i < _abilities.Length; i++)
        {
            if (_abilities[i] != null & _levels[i] > 0)
            {
                if (_abilities[i].AbilityData.GetAbilityType() != AbilityData.Type.Passive)                           
                {
                    BaseAbilityData baseAbilityData = (BaseAbilityData)_abilities[i].AbilityData;

                    float castMultiplier = 1;
                    if (_unit.Curses.Have(CursesInfo.List.Forest))
                    {
                        Curse forest = _unit.Curses.Find(CursesInfo.List.Forest);
                        castMultiplier = 1 - CursesInfo.Forest.CastSpeedMultiplier * forest.Effect / 100;
                    }

                    _reloadTimes[i] += deltaTime;
                    float castPerSecond = baseAbilityData.CastPerSecond.Get(_levels[i]) + _abilityModifiers[i].CastPerSecond;
                    castPerSecond *= castMultiplier;

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

                        while (_reloadTimes[i] >= attackSpeed)
                        {
                            _reloadTimes[i] -= 1 / castPerSecond;
                            OnCastReloaded?.Invoke(_abilities[i], i, _levels[i]);
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
                if (_abilities[0].AbilityData.GetAbilityType() == AbilityData.Type.Base
                    | _abilities[0].AbilityData.GetAbilityType() == AbilityData.Type.Range)
                {
                    BaseAbilityData baseAbilityData = (BaseAbilityData)_abilities[0].AbilityData;

                    _casted[0] = false;
                    _reloadTimes[0] = 0;
                    float attackSpeed = _animationAttackSpeed * baseAbilityData.CastPerSecond.Get(_levels[0]);
                    OnLevelUpAttack?.Invoke(attackSpeed);
                }
            }

            if (_abilities[index].AbilityData.GetAbilityType() == AbilityData.Type.Passive)
            {
                _casted[index] = false;
                OnLevelUpPassive?.Invoke(_abilities[index]);
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