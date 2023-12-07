using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseContext : MonoBehaviour, IContext
{
    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;
    public event Action OnPauseGame;
    public event Action OnContinueGame;

    private GameObject _damageIndication;
    private GameObject _damageIndicationParent;

    private MapTransferData _mapTransferData;

    public bool Paused { get; private set; }

    public IUnitPresenter Character => SetCharacter();

    public GameObject DamageIndication => _damageIndication;
    public GameObject DamageIndicationParent => _damageIndicationParent;

    public MapTransferData MapTransferData => _mapTransferData;

    private void Awake()
    {
        _damageIndication = Resources.Load<GameObject>("Damage");
        _damageIndicationParent = GameObject.FindWithTag("Damage Indication");
        //_mapTransferData = MapTransferData.Load();
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

    public void GoToMap(MapTransferData data)
    {
        data.Save();
        SceneManager.LoadScene(data.SelectedMap.ToString(), LoadSceneMode.Single);
    }
}
