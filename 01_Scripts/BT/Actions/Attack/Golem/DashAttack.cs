using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;

public class DashAttack : ActionBase
{
    public float animationChange = 1f;
    public override void OnStart()
    {
        _enemyBase.AnimatorCompo.CrossFadeInFixedTime("Atk_Dash", animationChange);
        _enemyBase.CanRotate = true;
    }

   

    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsStun || _enemyBase.IsDie) return TaskStatus.Failure;
        if (AnimationEnd("Atk_Dash", 0.55f) != true)
        {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
}