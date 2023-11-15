using System;
using UnityEngine;

public abstract class BaseContext : MonoBehaviour, IContext
{
    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;
    public event Action OnPauseGame;
    public event Action OnContinueGame;

    public bool Paused { get; private set; }

    public IUnitPresenter Character => SetCharacter();

    private void Awake()
    {
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

    
}
