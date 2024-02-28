[System.Serializable]
public class Curse
{
    public Curses.List Name;
    public float Duration;
    public float Effect;

    public Curse(Curses.List name, float effect, float duration)
    {
        Name = name;
        Duration = duration;
        Effect = effect;
    }
}