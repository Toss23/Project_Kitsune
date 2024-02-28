public class WeaknessAura : HitAbility
{
    protected override void OnHitEnemy(Unit enemy)
    {
        base.OnHitEnemy(enemy);

        enemy.CursesContainer.Add(new Curse(Curses.List.Weakness, Properties["Effect"], Properties["Duration"]));
    }
}