public class WeaknessAura : HitAbility
{
    protected override void OnHitEnemy(Unit enemy)
    {
        base.OnHitEnemy(enemy);

        enemy.Curses.Add(new Curse(CursesInfo.List.Weakness, Properties["Effect"], Properties["Duration"]));
    }
}