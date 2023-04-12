using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class EnemyPresenter : UnitPresenter
{
    private Transform _target;

    protected override IUnit CreateUnit() => new Enemy(_info, _target, _rigidbody);
    protected override IUnitView CreateUnitView() => GetComponent<EnemyView>();
    protected override bool IsCharacter() => false;

    protected override void BeforeAwake()
    {
        _target = GameObject.FindGameObjectWithTag("Character").transform;
    }

    protected override void AfterAwake()
    {
        Enable();
    }

    protected override void OnEnablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.OnMove += _unitView.SetAngle;
        follower.IsMoving += _unitView.IsMoving;
    }

    protected override void OnDisablePresenter()
    {
        
    }
}