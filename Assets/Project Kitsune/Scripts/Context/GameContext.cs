using UnityEngine;

public class GameContext : BaseContext
{
    [SerializeField] private AbilitiesSelectionPresenter _abilitiesSelectionPresenter;
    [SerializeField] private EnemySpawnerPresenter _enemySpawnerPresenter;
    [SerializeField] private AdminPresenter _adminPresenter;
    [SerializeField] private TimerPresenter _timerPresenter;
    [SerializeField] private KillCounterPresenter _killCounterPresenter;

    public IAbilitiesSelectionPresenter AbilitiesSelectionPresenter => _abilitiesSelectionPresenter;
    public IEnemySpawnerPresenter EnemySpawnerPresenter => _enemySpawnerPresenter;
    public IKillCounterPresenter KillCounterPresenter => _killCounterPresenter;

    protected override IUnitPresenter SetCharacter() => CharacterPresenter;

    protected override void OnLoadGame()
    {
        _abilitiesSelectionPresenter.Init(this, Character);
        Message("Abilities initialized...");

        _enemySpawnerPresenter.Init(this, Character);
        Message("Enemy Spawner initialized...");

        _controlable = CharacterPresenter.Character.Controllable;
        _unitView = CharacterPresenter.UnitView;
        _unit = CharacterPresenter.Unit;
        Message("Got references...");

        _adminPresenter.Init(CharacterPresenter);
        Message("Admin Panel initialized...");

        _timerPresenter.Init(this);
        Message("Timer initialized...");

        _killCounterPresenter.Init();
        Message("Kill Counter initialized...");
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