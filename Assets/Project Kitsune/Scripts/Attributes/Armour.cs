public class Armour : Attribute
{
    public Armour(float baseValue)
    {
        Value = baseValue;
        Minimum = 0;
        Maximum = 90;
    }

    protected override bool ClampOnChange() => false;
}