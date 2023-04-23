using UnityEngine;

[RequireComponent(typeof(AbilitiesSelectionView))]
public class AbilitiesSelectionPresenter : MonoBehaviour
{
    private IAbilitiesSelectionView _abilitiesSelectionView;
    private AbilitiesSelection _abilitiesSelection;
    private IUnit _unit;

    public AbilitiesSelection AbilitiesSelection => _abilitiesSelection;

    public void Init(IUnitPresenter characterPresenter)
    {
        _abilitiesSelectionView = GetComponent<AbilitiesSelectionView>();
        _unit = characterPresenter.Unit;
        _abilitiesSelection = new AbilitiesSelection(_unit);

        Enable();
    }

    private void Enable()
    {
        _unit.OnLevelUp += _abilitiesSelection.AbilityLevelUp;
        _abilitiesSelection.OnAbilitiesListGenerated += _abilitiesSelectionView.Build;
        _abilitiesSelectionView.OnSelected += _unit.Abilities.LevelUp;
        _abilitiesSelectionView.OnSelected += (ability) => _abilitiesSelection.CheckRequirementAbilityUp();
    }

    private void Disable()
    {
        _unit.OnLevelUp -= _abilitiesSelection.AbilityLevelUp;
        _abilitiesSelection.OnAbilitiesListGenerated -= _abilitiesSelectionView.Build;
        _abilitiesSelectionView.OnSelected -= _unit.Abilities.LevelUp;
        _abilitiesSelectionView.OnSelected -= (ability) => _abilitiesSelection.CheckRequirementAbilityUp();
    }
}