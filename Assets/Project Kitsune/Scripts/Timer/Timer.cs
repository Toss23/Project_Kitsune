using System;

public class Timer
{
    public event Action<int> OnSecondsChanged;

    private float _timer;
    private int _seconds;

    public Timer()
    {
        _seconds = 0;
    }

    public void Update(float deltaTime)
    {
        _timer += deltaTime;

        while (_timer >= 1f)
        {
            _timer -= 1f;
            _seconds += 1;
            OnSecondsChanged?.Invoke(_seconds);
        }
    }
}