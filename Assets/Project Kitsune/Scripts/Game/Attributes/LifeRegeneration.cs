public class LifeRegeneration : Attribute
{
    public LifeRegeneration(float value)
    {
        Value = value;
        Minimum = 0;
        Maximum = 100;
    }

    protected override bool ClampOnChange() => false;
}