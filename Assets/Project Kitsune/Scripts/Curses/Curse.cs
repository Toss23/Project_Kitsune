[System.Serializable]
public class Curse
{
    public CursesInfo.List Name;
    public float Duration;
    public float Effect;

    public Curse(CursesInfo.List name, float effect, float duration)
    {
        Name = name;
        Duration = duration;
        Effect = effect;
    }
}