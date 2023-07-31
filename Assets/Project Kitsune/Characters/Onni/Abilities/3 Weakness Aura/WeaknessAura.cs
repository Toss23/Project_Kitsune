public class WeaknessAura : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit caster, IUnit target)
    {
        target.Curses.Add(new Curse(CursesInfo.List.Weakness, Properties["Duration"], Properties["Effect"]));
    }

    protected override void OnCollisionStayWithEnemy(IUnit caster, IUnit target)
    {
        
    }

    protected override void OnCreate()
    {
        
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }
}