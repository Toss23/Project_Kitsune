using System;

public interface ILogic
{
    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;
    public event Action OnPauseGame;
    public event Action OnContinueGame;

    public IUnitPresenter Character { get; }
    public bool Paused { get; }

    public void PauseGame();
    public void ContinueGame();
    public void EndGame();
}