using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseContext : MonoBehaviour, IContext
{
    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;
    public event Action OnPauseGame;
    public event Action OnContinueGame;

    [SerializeField] private CharacterPresenter _characterPresenter;
    [SerializeField] private string[] _assetsName;

    private GameObject _damageIndication;
    private GameObject _damageIndicationParent;
    private MapData _mapData;

    private AssetLoader _assetLoader;

    protected CharacterPresenter CharacterPresenter => _characterPresenter;

    public bool Paused { get; private set; }

    public IUnitPresenter Character => SetCharacter();
    public AssetLoader AssetLoader => _assetLoader;

    public GameObject DamageIndication => _damageIndication;
    public GameObject DamageIndicationParent => _damageIndicationParent;

    public MapData MapData => _mapData;

    private async void Awake()
    {
        _assetLoader = new AssetLoader();

        if (_assetsName != null)
        {
            foreach (string name in _assetsName)
            {
                await _assetLoader.Load(name);
            }
        }

        foreach (string name in Configs.DefaultAssets)
        {
            await _assetLoader.Load(name);
        }

        _damageIndication = (GameObject)_assetLoader.GetHandle("Damage").Result;
        _damageIndicationParent = GameObject.FindWithTag("Damage Indication");
        _mapData = new MapData();

        // Select Charater
        CharacterPresenter.Init(this, UnitType.Character);
        Message("Character initialized...");

        Message("First initialization...");

        OnLoadGame();
    }

    private void Update()
    {
        if (Paused == false)
            OnUpdate?.Invoke(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (Paused == false)
            OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }

    protected abstract void OnLoadGame();

    protected abstract IUnitPresenter SetCharacter();
    
    public void PauseGame()
    {
        if (Paused == false)
        {
            Paused = true;
            OnPause();
            OnPauseGame?.Invoke();
        }
    }

    protected abstract void OnPause();

    public void ContinueGame()
    {
        if (Paused == true)
        {
            Paused = false;
            OnContinue();
            OnContinueGame?.Invoke();
        }
    }

    protected abstract void OnContinue();

    public void EndGame()
    {
        PauseGame();
    }

    protected void Message(string text)
    {
        Debug.Log("[Context] " + text);
    }

    protected void ErrorMessage(string text)
    {
        Debug.LogError("[Context] " + text);
    }

    public void GoToMap(Configs.Map map)
    {
        _assetLoader.ReleaseAll();

        _mapData.SelectedMap = map;
        _mapData.Save();
        SceneManager.LoadScene(map.ToString(), LoadSceneMode.Single);
    }
}
