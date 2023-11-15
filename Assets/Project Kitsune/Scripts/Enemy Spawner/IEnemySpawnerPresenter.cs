public interface IEnemySpawnerPresenter
{
    public void Init(IContext logic, IUnitPresenter character);
    public void Enable();
    public void Disable();
}