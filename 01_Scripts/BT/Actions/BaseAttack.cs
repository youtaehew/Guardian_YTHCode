using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BaseAttack : ActionBase
{
    [SerializeField] private string _animationName;
    [SerializeField] private float _changeAnimationTime = 0.65f;


    public  override void OnStart()
    {
        _enemyBase.CanRotate = true;
        _enemyBase.RotateSpeed = 6f;
        _enemyBase.AnimatorCompo.CrossFadeInFixedTime(_animationName, 1f);
    }


    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsStun || _enemyBase.IsDie) return TaskStatus.Failure;
        if (AnimationEnd(_animationName, _changeAnimationTime) != true)
        {
            return TaskStatus.Running;
        }

        return TaskStatus.Success;
    }
}