public interface IAbilitiesSelectionPresenter
{
    public void Init(ILogic logic, IUnitPresenter characterPresenter);
    public void Enable();
    public void Disable();
}