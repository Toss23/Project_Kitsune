using System;
using System.Collections.Generic;

public class CursesContainer
{
    public event Action<Curse> OnCurseIsEnded;

    private Dictionary<Curse, float> _curses;

    public CursesContainer()
    {
        _curses = new Dictionary<Curse, float>();
    }

    public void Add(Curse curse, float time, bool stack)
    {
        if (_curses.ContainsKey(curse))
        {
            if (stack)
            {
                if (_curses[curse] < time)
                    _curses[curse] = time;
            }
            else
            {
                _curses.Add(curse, time);
            }
        }
        else
        {
            _curses.Add(curse, time);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var curse in _curses)
        {
            _curses[curse.Key] -= deltaTime;
            if (_curses[curse.Key] <= 0)
            {
                OnCurseIsEnded?.Invoke(curse.Key);
                _curses.Remove(curse.Key);
            }
        }
    }
}