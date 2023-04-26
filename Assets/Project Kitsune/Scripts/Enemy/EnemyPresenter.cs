using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public class EnemyPresenter : UnitPresenter
{
    [SerializeField] private GameObject _enemyObject;
    [SerializeField] private float _experience;

    private GameObject _target;
    private CharacterPresenter _characterPresenter;

    protected override IUnit CreateUnit() => new Enemy(_info, _target.transform, _rigidbody);
    protected override IUnitView CreateUnitView() => GetComponent<EnemyView>();
    protected override bool IsCharacter() => false;
    protected override GameObject NonCharacterUnit() => _enemyObject;

    protected override void BeforeAwake()
    {
        _target = GameObject.FindGameObjectWithTag("Character");
        _characterPresenter = _target.GetComponent<CharacterPresenter>();
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

        _characterPresenter.OnFreeze += follower.Freeze;
        _characterPresenter.OnFreeze += _unit.Abilities.Freeze;
        _characterPresenter.OnFreeze += _unit.Immune;
    }

    protected override void OnDisablePresenter()
    {
        Follower follower = ((Enemy)_unit).Follower;
        follower.OnMove -= _unitView.SetAngle;
        follower.IsMoving -= _unitView.IsMoving;

        _characterPresenter.OnFreeze -= follower.Freeze;
        _characterPresenter.OnFreeze -= _unit.Abilities.Freeze;
        _characterPresenter.OnFreeze -= _unit.Immune;
    }

    protected override void OnDeath()
    {
        IUnitPresenter characterPresenter = _target.GetComponent<IUnitPresenter>();
        characterPresenter.Unit.Attributes.Level.AddExperience(_experience);
    }
}