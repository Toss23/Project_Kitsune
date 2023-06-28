using System;
using System.Collections.Generic;

public class CursesContainer
{
    public event Action<Curse> OnCursed;
    public event Action<Curse> OnCurseCleared;

    private List<Curse> _curses;

    public CursesContainer()
    {
        _curses = new List<Curse>();
    }

    public void Add(Curse curse)
    {     
        if (_curses.Exists(i => i.Type == curse.Type))
        {
            Curse prevCurse = _curses.Find(i => i.Type == curse.Type);
            prevCurse.Effect = Math.Max(prevCurse.Effect, curse.Effect);
            prevCurse.Duration = curse.Duration;
        }
        else
        {
            _curses.Add(curse);
            OnCursed?.Invoke(curse);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (Curse curse in _curses)
        {
            curse.Duration -= deltaTime;
            if (curse.Duration <= 0)
            {
                OnCurseCleared?.Invoke(curse);
                _curses.Remove(curse);
            }
        }
    }
}