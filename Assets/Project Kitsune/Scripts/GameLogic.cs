using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour, IGameLogic
{
    public static IGameLogic Instance { get; private set; }

    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;

    [SerializeField] private CharacterPresenter _character;
    [SerializeField] private AbilitiesSelectionPresenter _abilitiesSelection;

    private bool _paused = false;

    public IUnitPresenter Character => _character;
    public IAbilitiesSelectionPresenter AbilitiesSelection => _abilitiesSelection;
    public bool Paused => _paused;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadGame();
    }

    private void Update()
    {
        if (_paused == false)
            OnUpdate?.Invoke(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_paused == false)
            OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }

    public void LoadGame()
    {
        // Init Character
        Character.Init(UnitType.Character);
        Debug.Log("[GL] Character initialized...");

        // Init Ability Selection
        _abilitiesSelection.Init(Character);
        Debug.Log("[GL] Abilities Selection initialized...");

        // Get References
        _controlable = _character.UnitCharacter.Controlable;
        _unitView = _character.UnitView;
        _unit = _character.Unit;
        Debug.Log("[GL] Got references...");
    }

    // Reference for Pause
    private Controlable _controlable;
    private IUnitView _unitView;
    private IUnit _unit;

    public void PauseGame()
    {
        _paused = true;
        _controlable.SetActive(false);
        _unitView.SetActive(false);
        _unit.Immune(true);
    }

    public void ContinueGame()
    {
        _paused = false;
        _controlable.SetActive(true);
        _unitView.SetActive(true);
        _unit.Immune(false);
    }

    public void EndGame()
    {
        Instance = null;
        SceneManager.LoadScene("Game");
    }
}