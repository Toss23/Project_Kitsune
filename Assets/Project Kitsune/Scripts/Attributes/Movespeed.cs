public class Movespeed : Attribute
{
    public Movespeed(float baseValue)
    {
        Value = baseValue;
        Minimum = 0;
        Maximum = 10;
    }
}