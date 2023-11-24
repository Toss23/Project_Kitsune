using System;

public class KillCounter
{
    public event Action<int> OnChange;

    private int _kills;

    public KillCounter()
    {
        _kills = 0;
    }

    public void AddKill()
    {
        _kills += 1;
        OnChange?.Invoke(_kills);
    }
}
