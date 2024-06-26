using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine.AI;
using Unity.VisualScripting;

public class CombatAction : ActionBase
{
    public float MultiplyTime;
    private float _time;
    private float _dot;
    private float _combatNumber = 0;

    public override void OnStart()
    {
        _enemyBase.RotateSpeed = 3f;
        _enemyBase.StartJump = true;
        _enemyBase.CanRotate = true;
        _time = Random.Range((float)Owner.GetVariable("CombatMinTime").GetValue(), 
            (float)Owner.GetVariable("CombatMaxTime").GetValue());
        _enemyBase.AnimatorCompo.SetBool("isCombat", true);
        _enemyBase.NavMeshAgentCompo.enabled = false;
    }

    private void GetDot()
    {
        Vector3 dirToTarget = (_enemyBase.Player.transform.position - _enemyBase.transform.position).normalized;
        Vector3 forward = _enemyBase.transform.forward.normalized;
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

        _dot = Vector3.Dot(right, dirToTarget);
    }

    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsStun || _enemyBase.IsDie)
        {
            return TaskStatus.Failure;
        }
        _enemyBase.AnimatorCompo.SetFloat("CombatNumber", _combatNumber);
        GetDot();
        if (_dot > 0)
        {
            _combatNumber -= Time.deltaTime * MultiplyTime;
        }
        else if (_dot < 0)
        {
            _combatNumber += Time.deltaTime * MultiplyTime;
        }
        Mathf.Clamp(_combatNumber, -1f, 1);


        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _enemyBase.AnimatorCompo.SetBool("isCombat", false);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Running;
        }
    }


    public override void OnEnd()
    {
        base.OnEnd();
    }
}