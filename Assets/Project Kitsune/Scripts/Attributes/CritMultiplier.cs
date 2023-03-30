public class CritMultiplier : Attribute
{
    public CritMultiplier(float baseValue)
    {
        BaseValue = baseValue;
        Value = 0;
        Minimum = 0;
        Maximum = 450;
    }
}