using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class EnemyPresenter : UnitPresenter
{
    protected override IUnit CreateUnit() => new Enemy(_info);
    protected override IUnitView CreateUnitView() => GetComponent<EnemyView>();
    protected override bool IsCharacter() => false;

    protected override void OnAwake()
    {
        Enable();
    }
    protected override void OnEnablePresenter()
    {
        
    }

    protected override void OnDisablePresenter()
    {
        
    }
}