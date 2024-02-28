public static class Curses
{
    public enum List
    {
        Weakness, Forest, Shadow, Exposure
    }

    public static readonly Weakness Weakness = new Weakness();
    public static readonly Forest Forest = new Forest();
    public static readonly Shadow Shadow = new Shadow();
    public static readonly Exposure Exposure = new Exposure();
}

public class Weakness
{
    public float OutputDamageMultiplier = 0.5f;     // 50% reduce Deal Damage
    public float InputDamageMultiplier = 2f;        // 200% increased Damage Taken
}

public class Forest
{
    public float ActionSpeedMultiplier = 0.5f;      // 50% reduce Action Second
}

public class Shadow
{

}

public class Exposure
{

}