using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ElfRoll : ActionBase
{
    public bool AttackRotate = false;
    float _dot;
    public override void OnStart()
    {
        _enemyBase.NavMeshAgentCompo.enabled = false;
        _enemyBase.CanRotate = false;
        if (!AttackRotate)
        {
            //GetDot();
            //if (dot > 0.6f)
            //{
            //    _enemyBase.AnimatorCompo.SetFloat("RollNumber", 1f);
            //}
            //else if (dot < -.6f)
            //{
            //    _enemyBase.AnimatorCompo.SetFloat("RollNumber", -1f);
            //}
            //else
            //{
            //    _enemyBase.AnimatorCompo.SetFloat("RollNumber", 2);
            //}
            int number = Random.Range(-1, 1);
            _enemyBase.AnimatorCompo.SetFloat("RollNumber", number);
        }
        else
        {
            _enemyBase.AnimatorCompo.SetFloat("RollNumber", 0);
        }
        _enemyBase.AnimatorCompo.CrossFadeInFixedTime("Roll", .2f);
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
        if (_enemyBase.IsStun || _enemyBase.IsDie) return TaskStatus.Failure;
        if (AnimationEnd("Roll", 0.8f) != true)
        {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }
}