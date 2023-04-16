public class Experience : Attribute
{
    public static readonly float[] MaximumList = new float[]
    {
       0, 10, 50, 100, 200, 0
    };

    public Experience(int level)
    {
        NextLevel(level);
    }

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