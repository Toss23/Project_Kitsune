using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class CharacterPresenter : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ProgressBar _lifeBar;
    [SerializeField] private ProgressBar _experienceBar;

    private ICharacterView _characterView;
    private ICharacter _character;

    private bool isEnable = false;

    private void Awake()
    {
        _characterView = GetComponent<CharacterView>();
        _character = new Character();

        Enable();
    }

    private void Enable()
    {
        isEnable = true;

        Controlable controlable = _character.Controlable;
        _joystick.OnActive += (angle, deltaTime) => controlable.Move(angle, deltaTime);
        controlable.OnMove += (position) => _characterView.SetPosition(position);

        Life life = _character.Attributes.Life;
        _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());
        life.OnLifeChange += (value) => _lifeBar.SetPercentAndText(life.GetPercent(), life.ToString());

        Level level = _character.Attributes.Level;
        _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
        level.OnExperienceChanged += (value) => _experienceBar.SetPercentAndText(level.GetPercent(), level.ToString());
    }

    private float _timer;

    private void Update()
    {
        if (isEnable)
        {
            _character.Update(Time.deltaTime);

            _timer += Time.deltaTime;
            while (_timer >= 1)
            {
                _timer--;
                _character.Attributes.Level.AddExperience(1);
            }
        }
    }
}