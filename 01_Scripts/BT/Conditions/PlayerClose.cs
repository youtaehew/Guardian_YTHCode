using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class PlayerClose : ConditionalBase
{
    public float MinDis = 0.8f;
    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsStun) return TaskStatus.Failure;
        if (_enemyBase.DistancePlayer < MinDis)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}