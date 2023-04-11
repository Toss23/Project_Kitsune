using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class CharacterPresenter : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ProgressBar _lifeBar;
    [SerializeField] private ProgressBar _experienceBar;
    [SerializeField] private CharacterInfo _characterInfo;

    private ICharacterView _characterView;
    private ICharacter _character;

    private bool isEnable = false;

    private void Awake()
    {
        _characterView = GetComponent<CharacterView>();
        _characterView.SpawnCharacter(_characterInfo.Prefab);
        _character = new Character(_characterInfo);

        Enable();
    }

    private void Enable()
    {
        isEnable = true;

        Controlable controlable = _character.Controlable;
        _joystick.OnActive += (angle, deltaTime) => controlable.Move(angle, deltaTime);
        _joystick.OnActive += (angle, deltaTime) => _characterView.SetAngle(angle);
        _joystick.IsActive += (active) => _characterView.Move(active);
        controlable.OnMove += (position) => _characterView.SetPosition(position);

        Life life = _character.Attributes.Life;
        _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());
        life.OnLifeChange += (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Level level = _character.Attributes.Level;
        _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged += (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());

        _character.Abilities.OnCastReloaded += CreateAbility;
    }

    private void CreateAbility(IAbility ability, AbilityType type, int level)
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
                abilityObject.Init(level);
                Transform abilityTransform = abilityObject.gameObject.transform;
                abilityTransform.position = _characterView.AbilityPoints.Points[(int)type].transform.position;
                abilityTransform.Rotate(new Vector3(0, 0, _characterView.Angle + startAngle+ deltaAngle * i));

                if (ability.Info.AbilityType == AbilityInfo.Type.Melee
                    || ability.Info.AbilityType == AbilityInfo.Type.Field)
                    abilityObject.FuseWith(_characterView.AbilityPoints.Points[(int)type].transform);

                _character.RegisterAbility(abilityObject);
            }
        }
    }

    private void Update()
    {
        if (isEnable)
        {
            _character.Update(Time.deltaTime);
        }
    }
}