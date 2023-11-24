using System;
using UnityEngine;

public interface IContext
{
    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;
    public event Action OnPauseGame;
    public event Action OnContinueGame;

    public IUnitPresenter Character { get; }
    public bool Paused { get; }

    public GameObject DamageIndication { get; }
    public GameObject DamageIndicationParent { get; }

    public void PauseGame();
    public void ContinueGame();
    public void EndGame();
    public void GoToMap(MapTransferData data);
}