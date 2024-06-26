using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;

public class JumpAttack : ActionBase
{
    private Vector3 _playerPos;
    [SerializeField] private AnimationCurve _jumpAnimationCurve;

    public override void OnStart()
    {
        // _enemyBase.LegsAnimatorCompo.User_FadeToDisabled(.5f);
        _enemyBase.CanRotate = true;
        _enemyBase.NavMeshAgentCompo.enabled = false;
        _enemyBase.AnimatorCompo.CrossFadeInFixedTime("Atk_Jump", 1f);
        _enemyBase.CanJump = false;
    }


    private void Jump()
    {
        _playerPos = _enemyBase.Player.transform.position;
        _playerPos.y = 0;
        Vector3[] movePos =
        {
            _enemyBase.transform.position,
            Vector3.Lerp(_enemyBase.transform.position, _playerPos, 0.8f) + Vector3.up * 3f,
            _playerPos
        };

        _enemyBase.transform.DOPath(movePos, 1f, pathType: PathType.CatmullRom).SetEase(_jumpAnimationCurve);
        _enemyBase.CanJump = false;
    }

    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsStun || _enemyBase.IsDie)
        {
            return TaskStatus.Failure;
        }

        if (_enemyBase.CanJump)
        {
            Jump();
        }

        if (AnimationEnd("Atk_Jump", 0.6f) != true)
        {
            return TaskStatus.Running;
        }

        // _enemyBase.LegsAnimatorCompo.User_FadeEnabled(.5f);
        return TaskStatus.Success;
    }
}