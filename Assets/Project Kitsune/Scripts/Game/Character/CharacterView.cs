using UnityEngine;

public class CharacterView : UnitView, ICharacterView
{
    [Header("UI")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ProgressBar _lifeBar;
    [SerializeField] private ProgressBar _magicShieldBar;
    [SerializeField] private ProgressBar _experienceBar;
    [SerializeField] private AbilitiesSelectionPresenter _abilitiesSelectionPresenter;

    public Joystick Joystick => _joystick;
    public ProgressBar LifeBar => _lifeBar;
    public ProgressBar MagicShieldBar => _magicShieldBar;
    public ProgressBar ExperienceBar => _experienceBar;
    public AbilitiesSelectionPresenter AbilitiesSelectionPresenter => _abilitiesSelectionPresenter;
}