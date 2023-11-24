public interface IAbilitiesSelectionPresenter
{
    public void Init(IContext logic, IUnitPresenter characterPresenter);
    public void Enable();
    public void Disable();
}