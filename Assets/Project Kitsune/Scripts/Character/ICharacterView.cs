public interface ICharacterView
{
    public Joystick Joystick { get; }
    public ProgressBar LifeBar { get; }
    public ProgressBar MagicShieldBar { get; }
    public ProgressBar ExperienceBar { get; }
    public AbilitiesSelectionPresenter AbilitiesSelectionPresenter { get; }
}
