using UnityEngine;

[RequireComponent(typeof(AbilitiesSelectionView))]
public class AbilitiesSelectionPresenter : MonoBehaviour, IAbilitiesSelectionPresenter
{
    private IAbilitiesSelectionView _abilitiesSelectionView;
    private AbilitiesSelection _abilitiesSelection;
    private Unit _character;
    private IContext _logic;

    public AbilitiesSelection AbilitiesSelection => _abilitiesSelection;

    public void Init(IContext logic, IUnitPresenter characterPresenter)
    {
        _abilitiesSelectionView = GetComponent<AbilitiesSelectionView>();
        _character = characterPresenter.Unit;
        _abilitiesSelection = new AbilitiesSelection(_character);
        _logic = logic;
        Enable();
    }

    public void Enable()
    {
        _abilitiesSelection.OnAbilitiesListGenerated += (abilities, levels) => _logic.PauseGame();
        _abilitiesSelection.OnAbilityUpCanceled += _logic.ContinueGame;

        _character.OnLevelUp += _abilitiesSelection.AbilityLevelUp;
        _abilitiesSelection.OnAbilitiesListGenerated += _abilitiesSelectionView.Build;
        _abilitiesSelectionView.OnSelected += _character.Abilities.LevelUp;
        _abilitiesSelectionView.OnSelected += (ability) => _abilitiesSelection.CheckRequirementAbilityUp();
    }

    public void Disable()
    {
        _abilitiesSelection.OnAbilitiesListGenerated -= (abilities, levels) => _logic.PauseGame();
        _abilitiesSelection.OnAbilityUpCanceled -= _logic.ContinueGame;

        _character.OnLevelUp -= _abilitiesSelection.AbilityLevelUp;
        _abilitiesSelection.OnAbilitiesListGenerated -= _abilitiesSelectionView.Build;
        _abilitiesSelectionView.OnSelected -= _character.Abilities.LevelUp;
        _abilitiesSelectionView.OnSelected -= (ability) => _abilitiesSelection.CheckRequirementAbilityUp();
    }
}