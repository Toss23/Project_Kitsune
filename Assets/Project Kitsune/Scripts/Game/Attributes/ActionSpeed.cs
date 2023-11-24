public class ActionSpeed : Attribute
{
    protected override bool ClampOnChange() => false;

    public ActionSpeed()
    {
        Value = 1f;
        Minimum = 0.1f;
        Maximum = 2f;
        SaveDefault();
    }
}