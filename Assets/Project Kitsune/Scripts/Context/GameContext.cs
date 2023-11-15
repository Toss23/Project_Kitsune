using UnityEngine;

public class GameContext : BaseContext
{
    [SerializeField] private CharacterPresenter _character;
    [SerializeField] private AbilitiesSelectionPresenter _abilitiesSelection;
    [SerializeField] private EnemySpawnerPresenter _enemySpawner;
    [SerializeField] private AdminPresenter _adminPresenter;

    public IAbilitiesSelectionPresenter AbilitiesSelection => _abilitiesSelection;
    public IEnemySpawnerPresenter EnemySpawner => _enemySpawner;
    protected override IUnitPresenter SetCharacter() => _character;

    protected override void OnLoadGame()
    {
        Character.Init(this, UnitType.Character);
        Debug.Log("[GL] Character initialized...");

        AbilitiesSelection.Init(this, Character);
        Debug.Log("[GL] Abilities Selection initialized...");

        EnemySpawner.Init(this, Character);
        Debug.Log("[GL] Enemy Spawner initialized...");

        _controlable = _character.Character.Controllable;
        _unitView = _character.UnitView;
        _unit = _character.Unit;
        Debug.Log("[GL] Got references...");

        _adminPresenter.Init();
        Debug.Log("[GL] Admin Panel initialized...");
    }

    // Reference for Pause
    private Controllable _controlable;
    private IUnitView _unitView;
    private Unit _unit;

    protected override void OnPause()
    {
        _controlable.SetActive(false);
        _unitView.SetActive(false);
        _unit.Immune(true);
    }

    protected override void OnContinue()
    {
        _controlable.SetActive(true);
        _unitView.SetActive(true);
        _unit.Immune(false);
    }
}