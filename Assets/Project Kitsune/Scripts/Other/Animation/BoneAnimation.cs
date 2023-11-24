using UnityEngine;

public class BoneAnimation : MonoBehaviour
{
    private enum Animation
    {
        Idle, Move, Attack, MoveAndAttack
    }

    [SerializeField] private Animation _selectedAnimation;

    private Animator _animator;

    private void Awake()
    {
        SetAnimation();
    }

    private void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            switch (_selectedAnimation)
            {
                case Animation.Idle:
                    _animator.SetBool("Moving", false);
                    _animator.SetBool("Attacking", false);
                    break;
                case Animation.Move:
                    _animator.SetBool("Moving", true);
                    _animator.SetBool("Attacking", false);
                    break;
                case Animation.Attack:
                    _animator.SetBool("Moving", false);
                    _animator.SetBool("Attacking", true);
                    break;
                case Animation.MoveAndAttack:
                    _animator.SetBool("Moving", true);
                    _animator.SetBool("Attacking", true);
                    break;
            }
        }
    }
}