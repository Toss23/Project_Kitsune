using UnityEngine;

[RequireComponent(typeof(TimerView))]
public class TimerPresenter : MonoBehaviour
{
    private GameContext _gameContext;
    private Timer _timer;
    private ITimerView _timerView;

    public void Init(GameContext gameContext)
    {
        _gameContext = gameContext;
        _timer = new Timer();
        _timerView = GetComponent<TimerView>();

        Enable();
    }

    public void Enable()
    {
        _gameContext.OnUpdate += _timer.Update;
        _timer.OnSecondsChanged += _timerView.SetTime;
    }

    public void Disable()
    {
        _gameContext.OnUpdate -= _timer.Update;
        _timer.OnSecondsChanged -= _timerView.SetTime;
    }
}
