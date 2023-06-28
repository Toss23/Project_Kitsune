public class WeaknessAura : Ability
{
    protected override void OnCollisionEnterWithEnemy(IUnit enemy)
    {
        enemy.Curses.Add(new Curse(Curse.CurseType.Weakness, 60000, Properties["Effect"]));
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