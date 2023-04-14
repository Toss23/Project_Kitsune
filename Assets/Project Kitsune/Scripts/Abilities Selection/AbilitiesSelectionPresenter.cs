using UnityEngine;

[RequireComponent(typeof(AbilitiesSelectionView))]
public class AbilitiesSelectionPresenter : MonoBehaviour
{
    private CharacterPresenter _characterPresenter;
    private IAbilitiesSelectionView _abilitiesSelectionView;
    private AbilitiesSelection _abilitiesSelection;
    private IUnitPresenter _unitPresenter;
    private IUnit _unit;

    public AbilitiesSelection AbilitiesSelection => _abilitiesSelection;

    public void Init(CharacterPresenter characterPresenter)
    {
        _characterPresenter = characterPresenter;
        _abilitiesSelectionView = GetComponent<AbilitiesSelectionView>();
        _unitPresenter = _characterPresenter;
        _unit = _unitPresenter.Unit;
        _abilitiesSelection = new AbilitiesSelection(_unit);

        Enable();
    }

    private void Enable()
    {
        _unit.OnLevelUp += _abilitiesSelection.GenerateAbilitiesList;
        _abilitiesSelection.OnAbilitiesListGenerated += _abilitiesSelectionView.Build;
        _abilitiesSelectionView.OnSelected += _unit.Abilities.LevelUp;
        _abilitiesSelectionView.OnSelected += _abilitiesSelection.OnSelected;
    }

    private void Disable()
    {
        _unit.OnLevelUp -= _abilitiesSelection.GenerateAbilitiesList;
        _abilitiesSelection.OnAbilitiesListGenerated -= _abilitiesSelectionView.Build;
        _abilitiesSelectionView.OnSelected -= _unit.Abilities.LevelUp;
        _abilitiesSelectionView.OnSelected -= _abilitiesSelection.OnSelected;
    }
}