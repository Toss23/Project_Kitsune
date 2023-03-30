public class Armour : Attribute
{
    public Armour(float baseValue)
    {
        BaseValue = baseValue;
        Value = 0;
        Minimum = 0;
        Maximum = 100;
    }
}