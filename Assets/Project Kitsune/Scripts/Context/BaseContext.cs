using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseContext : MonoBehaviour, IContext
{
    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;
    public event Action OnPauseGame;
    public event Action OnContinueGame;

    [SerializeField] private CharacterPresenter _characterPresenter;

    private GameObject _damageIndication;
    private GameObject _damageIndicationParent;
    private MapData _mapData;

    private List<AssetLoader> _assetLoaders;

    protected CharacterPresenter CharacterPresenter => _characterPresenter;

    public bool Paused { get; private set; }

    public IUnitPresenter Character => SetCharacter();

    public GameObject DamageIndication => _damageIndication;
    public GameObject DamageIndicationParent => _damageIndicationParent;

    public MapData MapData => _mapData;

    private void Awake()
    {
        _assetLoaders = new List<AssetLoader>();

        //_damageIndication = Resources.Load<GameObject>("Damage");
        _damageIndicationParent = GameObject.FindWithTag("Damage Indication");
        _mapData = new MapData();

        // Select Charater
        //
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

    public void GoToMap(Configs.Map map)
    {
        foreach (AssetLoader assetLoader in _assetLoaders)
        {
            assetLoader.ReleaseAll();
        }

        _mapData.SelectedMap = map;
        _mapData.Save();
        SceneManager.LoadScene(map.ToString(), LoadSceneMode.Single);
    }

    public void RegisterAssetLoader(AssetLoader assetLoader)
    {
        if (_assetLoaders.Contains(assetLoader) == false) {
            _assetLoaders.Add(assetLoader);
        }
    }
}
