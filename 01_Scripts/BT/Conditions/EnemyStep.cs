using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyStep : ConditionalBase
{

    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsSecondStep)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}