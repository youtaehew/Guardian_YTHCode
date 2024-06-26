using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanDash : ConditionalBase
{
	public float DashMinDis = 2.9f;
	public float DashMaxDis = 3.8f;
	public override TaskStatus OnUpdate()
	{
        if (_enemyBase.IsStun) return TaskStatus.Failure;
        if (_enemyBase.DistancePlayer <= DashMaxDis && _enemyBase.DistancePlayer >= DashMinDis && _enemyBase.StartJump)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}