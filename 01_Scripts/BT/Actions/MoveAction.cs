using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Diagnostics;
using UnityEngine.AI;

public class MoveAction : ActionBase
{
    private float _time;
    public float FarDis = 2.5f;
    public float CombatDis = 2;

    public override void OnStart()
    {
        _time = 0;
        _enemyBase.RotateSpeed = 3f;
        _enemyBase.NavMeshAgentCompo.enabled = true;
        _enemyBase.NavMeshAgentCompo.SetDestination(_enemyBase.Player.transform.position);
        _enemyBase.AnimatorCompo.SetBool("isMove", true);
    }

    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsStun || _enemyBase.IsDie)
        {
            return TaskStatus.Failure;
        }

        if (_enemyBase.DistancePlayer > FarDis && _enemyBase.StartJump)
        {
            _time += Time.deltaTime;
            if (_time >= 1)
            {
                _enemyBase.AnimatorCompo.SetBool("isMove", false);
                return TaskStatus.Failure;
            }
        }

        if (_enemyBase.DistanceDestination > 0.2f)
        {
            _enemyBase.NavMeshAgentCompo.SetDestination(_enemyBase.Player.transform.position);
        }

        if (_enemyBase.DistancePlayer <= CombatDis)
        {
            _enemyBase.AnimatorCompo.SetBool("isMove", false);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }
}