using System;
using UnityEngine;

public abstract class UnitView : MonoBehaviour, IUnitView
{
    [Header("Base Property")]
    [SerializeField] private Transform _delta;
    [SerializeField] private Transform _cursesPoint;

    private GameObject _unit;
    private Animator _animator;
    private AbilityPoints _abilityPoints;

    private bool _isMirrored;
    private float _angle;
    private bool _freeze = false;

    private GameObject[] _cursesIcon;

    public AbilityPoints AbilityPoints => _abilityPoints;
    public bool IsMirrored => _isMirrored;
    public float Angle => _angle;

    public void IsMoving(bool move)
    {
        if (_animator != null)
        {
            _animator.SetBool("Move", _freeze ? false : move);
        }
    }

    public void CreateUnit(GameObject prefab)
    {
        _unit = Instantiate(prefab, _delta);
        _unit.name = prefab.name;
        _animator = _unit.GetComponent<Animator>();
        _abilityPoints = _unit.GetComponent<AbilityPoints>();
        _isMirrored = false;
    }

    public void SetUnit(GameObject unit)
    {
        _unit = unit;
        _animator = _unit.GetComponent<Animator>();
        _abilityPoints = _unit.GetComponent<AbilityPoints>();
        _isMirrored = false;
    }

    public void SetAngle(float angle)
    {
        if (_freeze == false)
        {
            _angle = angle;
            _isMirrored = Mathf.Abs(angle) > 90;
            transform.rotation = Quaternion.Euler(0, _isMirrored ? 180 : 0, 0);
        }
    }

    public void Freeze(bool state)
    {
        _freeze = state;
    }

    public void SetAttackAnimationTime(float multiplier)
    {
        if (_animator != null)
        {
            _animator.SetFloat("AttackTimeMultiplier", multiplier);
        }
    }

    public void SetCurseIcon(Curse curse, bool active)
    {
        if (_cursesIcon == null)
        {
            int cursesCount = Enum.GetNames(typeof(CursesInfo.List)).Length;
            _cursesIcon = new GameObject[cursesCount + 1];

            _cursesIcon[_cursesIcon.Length - 1] = Instantiate(CursesInfo.CenterSprite, _cursesPoint);

            for (int i = 0; i < cursesCount; i++)
            {
                _cursesIcon[i] = Instantiate(CursesInfo.Spites[(CursesInfo.List)i], _cursesPoint);
                _cursesIcon[i].SetActive(false);
            }
        }

        _cursesIcon[(int)curse.Name].SetActive(active);

        bool haveCurse = false;
        foreach (GameObject curseIcon in _cursesIcon)
        {
            if (curseIcon.activeSelf == true)
            {
                haveCurse = true;
                break;
            }
        }
        _cursesIcon[_cursesIcon.Length - 1].SetActive(haveCurse);
    }
}