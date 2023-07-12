using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour, IGameLogic
{
    public static GameLogic Instance { get; private set; }

    public event Action<float> OnUpdate;
    public event Action<float> OnFixedUpdate;

    [SerializeField] private UnitPresenter _character;

    private bool _paused = false;

    public IUnitPresenter Character => _character;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        StartGame();
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

    public void StartGame()
    {
       // Init Character
       // Register OnDeath -> EndGame
       // Init Ability Card
       // Spawn Character
       // Init Spawner
    }

    public void PauseGame()
    {
        _paused = true;
    }

    public void ContinueGame()
    {
        _paused = false;
    }

    public void EndGame()
    {
        
    }
}