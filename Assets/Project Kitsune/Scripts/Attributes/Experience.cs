public class Experience : Attribute
{
    public static readonly float[] MaximumList = new float[]
    {
       0, 10, 15, 20, 30, 40, 50, 70, 100, 150, 200, 0
    };

    public Experience(int level)
    {
        NextLevel(level);
    }

    protected override bool ClampOnChange() => true;

    public void NextLevel(int level)
    {
        Value = 0;
        Minimum = 0;
        Maximum = MaximumList[level];
    }

    public override float GetPercent()
    {
        if (Maximum != 0)
            return base.GetPercent();
        else
            return 100;
    }
}