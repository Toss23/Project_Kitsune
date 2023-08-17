public interface IAbilityCaster
{
    public void Init(IUnitPresenter unitPresenter);
    public void CreateAbility(IAbility ability, int point, int level);
}