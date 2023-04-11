public class CritChance : Attribute
{
    public CritChance(float baseValue)
    {
        Value = baseValue;
        Minimum = 0;
        Maximum = 100 - baseValue;
    }
}