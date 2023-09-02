using System;
using UnityEngine;

public abstract class UnitView : MonoBehaviour, IUnitView
{
    [Header("Base Property")]
    [SerializeField] private int _sortingOrderOffset;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _cursesPoint;
    [SerializeField] private Transform _shieldPoint;

    private GameObject _unit;
    private Animator _animator;
    private IAbilityPoints _abilityPoints;

    private bool _isMirrored;
    private float _angle;
    private bool _active = true;
    private float _defaultActionSpeed;

    private GameObject[] _cursesIcon;
    private GameObject _magicShieldSprite;

    public IAbilityPoints AbilityPoints => _abilityPoints;
    public float Angle => _angle;

    public void CreateUnit(GameObject prefab, float defaultActionSpeed)
    {
        _unit = Instantiate(prefab, _spawnPoint);
        _unit.name = prefab.name;
        _animator = _unit.GetComponent<Animator>();
        _abilityPoints = _unit.GetComponent<AbilityPoints>();
        _isMirrored = false;
        _defaultActionSpeed = defaultActionSpeed;

        if (_animator != null)
        {
            _animator.SetBool("Attacking", true);
        }
    }

    public void IsMoving(bool move)
    {
        if (_animator != null)
        {
            _animator.SetBool("Moving", _active ? move : false);
        }
    }

    public void SetAngle(float angle)
    {
        if (_active)
        {
            _angle = angle;
            _isMirrored = Mathf.Abs(angle) > 90;
            transform.rotation = Quaternion.Euler(0, _isMirrored ? 180 : 0, 0);
        }
    }

    public void SetActive(bool active)
    {
        _active = active;
        _animator.SetBool("Attacking", active);
    }

    public void SetActionSpeed(float speed)
    {
        if (_animator != null)
        {
            _animator.SetFloat("ActionSpeed", speed * _defaultActionSpeed);
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
                _cursesIcon[i] = Instantiate(CursesInfo.Sprites[(CursesInfo.List)i], _cursesPoint);
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

    public void SetMagicShield(bool active)
    {
        if (_magicShieldSprite == null)
        {
            GameObject magicShield = Resources.Load<GameObject>("MagicShield");
            _magicShieldSprite = Instantiate(magicShield, _shieldPoint);
            _magicShieldSprite.name = "Magic Shield";
        }

        _magicShieldSprite.SetActive(active);
    }
}