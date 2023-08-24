using System;

public interface IAbilitiesSelectionView
{
    public event Action<IAbility> OnSelected;

    public void Show();
    public void Hide();
    public void Build(IAbility[] abilities, int[] levels);
}