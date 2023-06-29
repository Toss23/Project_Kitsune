using System;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Curses updated");
        foreach (Curse curse in _curses)
        {
            Debug.Log(curse.Name + " updated / " + curse.Duration);
            curse.Duration -= deltaTime;
            if (curse.Duration <= 0)
            {
                OnCurseCleared?.Invoke(curse);
                Debug.Log(curse.Name + " cleared");
                _curses.Remove(curse);
            }
        }
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