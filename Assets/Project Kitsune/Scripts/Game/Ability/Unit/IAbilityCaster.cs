public interface IAbilityCaster
{
    public void Init(IContext logic, IUnitPresenter unitPresenter);
    public void CreateAbility(IAbility ability, int point, int level);
}