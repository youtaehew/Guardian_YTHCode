using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanJump : ConditionalBase
{
    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsStun && _enemyBase.IsDie) return TaskStatus.Failure;
        if (_enemyBase.DistancePlayer <= 7 && _enemyBase.DistancePlayer >= 3.9 && _enemyBase.StartJump)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}