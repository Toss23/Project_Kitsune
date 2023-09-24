public interface IEnemySpawnerPresenter
{
    public void Init(ILogic logic, IUnitPresenter character);
    public void Enable();
    public void Disable();
}