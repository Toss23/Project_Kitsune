using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class EnemyPresenter : UnitPresenter
{
    private GameObject _target;

    protected override IUnit CreateUnit() => new Enemy(_info, _target.transform, _rigidbody);
    protected override IUnitView CreateUnitView() => GetComponent<EnemyView>();

    private void Awake()
    {
        _target = GameLogic.Instance.Character.Transform.gameObject;
    }

    protected override void OnEnablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.OnMove += _unitView.SetAngle;
        follower.IsMoving += _unitView.IsMoving;
    }

    protected override void OnDisablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.OnMove -= _unitView.SetAngle;
        follower.IsMoving -= _unitView.IsMoving;
    }

    protected override void OnDeath()
    {
        IUnitPresenter characterPresenter = _target.GetComponent<IUnitPresenter>();
        characterPresenter.Unit.Attributes.Level.AddExperience(Unit.ExperienceGain);
    }
}