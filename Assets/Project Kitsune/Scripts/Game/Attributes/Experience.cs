public class Experience : Attribute
{
    public Experience(int level)
    {
        NextLevel(level);
    }

    protected override bool ClampOnChange() => true;

    public void NextLevel(int level)
    {
        Value = 0;
        Minimum = 0;
        Maximum = Configs.Experience[level];
    }

    public override float GetPercent()
    {
        if (Maximum != 0)
            return base.GetPercent();
        else
            return 100;
    }
}