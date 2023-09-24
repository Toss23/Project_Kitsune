public interface IAbilityCaster
{
    public void Init(ILogic logic, IUnitPresenter unitPresenter);
    public void CreateAbility(IAbility ability, int point, int level);
}