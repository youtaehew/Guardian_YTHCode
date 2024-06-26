using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyStepOne : ConditionalBase
{
    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsSecondStep)
        {
            return TaskStatus.Failure;
        }

        return TaskStatus.Success;
    }
}