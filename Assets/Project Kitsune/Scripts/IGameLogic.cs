using System;

public interface IGameLogic
{
    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;

    public IUnitPresenter Character { get; }
    public IAbilitiesSelectionPresenter AbilitiesSelection { get; }
    public bool Paused { get; }

    public void LoadGame();
    public void PauseGame();
    public void ContinueGame();
    public void EndGame();
}