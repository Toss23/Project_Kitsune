public class CritMultiplier : Attribute
{
    public CritMultiplier(float baseValue)
    {
        Value = baseValue;
        Minimum = 0;
        Maximum = 450;
    }
}