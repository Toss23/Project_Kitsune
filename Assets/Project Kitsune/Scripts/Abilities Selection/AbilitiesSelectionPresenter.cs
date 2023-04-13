using UnityEngine;

[RequireComponent(typeof(AbilitiesSelectionView))]
public class AbilitiesSelectionPresenter : MonoBehaviour
{
    [SerializeField] private CharacterPresenter _characterPresenter;

    private IAbilitiesSelectionView _abilitiesSelectionView;
    private AbilitiesSelection _abilitiesSelection;
    private IUnitPresenter _unitPresenter;
    private IUnit _unit;

    private void Start()
    {
        _abilitiesSelectionView = GetComponent<AbilitiesSelectionView>();
        _unitPresenter = _characterPresenter;
        _unit = _unitPresenter.Unit;
        _abilitiesSelection = new AbilitiesSelection(_unit);

        Enable();
    }

    private void Enable()
    {
        _unit.OnLevelUp += _abilitiesSelection.Method;
        _abilitiesSelection.onMethod += _abilitiesSelectionView.Build;
        _abilitiesSelectionView.OnSelected += _unit.Abilities.LevelUp;
    }
}