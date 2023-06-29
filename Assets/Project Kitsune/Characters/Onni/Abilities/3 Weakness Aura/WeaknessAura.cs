public class WeaknessAura : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit enemy)
    {
        enemy.Curses.Add(new Curse(CursesInfo.List.Weakness, Properties["Duration"], Properties["Effect"]));
    }

    protected override void OnCollisionStayWithEnemy(IUnit enemy)
    {
        
    }

    protected override void OnCreate()
    {
        
    }

    protected override void OnUpdate(float deltaTime)
    {
        
    }
}