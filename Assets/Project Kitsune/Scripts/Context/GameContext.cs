using UnityEngine;

public class GameContext : BaseContext
{
    [SerializeField] private CharacterPresenter _character;
    [SerializeField] private AbilitiesSelectionPresenter _abilitiesSelection;
    [SerializeField] private EnemySpawnerPresenter _enemySpawner;
    [SerializeField] private AdminPresenter _adminPresenter;
    [SerializeField] private TimerPresenter _timerPresenter;

    public IAbilitiesSelectionPresenter AbilitiesSelection => _abilitiesSelection;
    public IEnemySpawnerPresenter EnemySpawner => _enemySpawner;
    protected override IUnitPresenter SetCharacter() => _character;

    protected override void OnLoadGame()
    {
        Character.Init(this, UnitType.Character);
        Message("Character initialized...");

        AbilitiesSelection.Init(this, Character);
        Message("Abilities initialized...");

        EnemySpawner.Init(this, Character);
        Message("Enemy Spawner initialized...");

        _controlable = _character.Character.Controllable;
        _unitView = _character.UnitView;
        _unit = _character.Unit;
        Message("Got references...");

        _adminPresenter.Init();
        Message("Admin Panel initialized...");

        _timerPresenter.Init(this);
        Message("Timer initialized...");
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