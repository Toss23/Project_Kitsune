public class CritChance : Attribute
{
    public CritChance(float baseValue)
    {
        BaseValue = baseValue;
        Value = 0;
        Minimum = 0;
        Maximum = 100 - baseValue;
    }
}