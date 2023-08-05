using System;
using System.Collections.Generic;

public class CursesContainer
{
    public event Action<Curse> OnCursed;
    public event Action<Curse> OnCurseCleared;

    private List<Curse> _curses;
    private List<Curse> _cursesToRemove;

    public CursesContainer()
    {
        _curses = new List<Curse>();
        _cursesToRemove = new List<Curse>();
    }

    public void Add(Curse curse)
    {     
        if (_curses.Exists(i => i.Name == curse.Name))
        {
            Curse prevCurse = _curses.Find(i => i.Name == curse.Name);
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
                _cursesToRemove.Add(curse);
            }
        }

        foreach (Curse curse in _cursesToRemove)
        {
            OnCurseCleared?.Invoke(curse);
            _curses.Remove(curse);
        }

        _cursesToRemove.Clear();
    }

    public bool Have(CursesInfo.List curse)
    {
        return _curses.Exists(i => i.Name == curse);
    }

    public Curse Find(CursesInfo.List curse)
    {
        return _curses.Find(i => i.Name == curse);
    }
}