using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class StunAction : ActionBase
{
    float _stunTime;
    public override void OnStart()
    {
        if (_enemyBase.IsDie)
        {
            Die();
        }
        else if (_enemyBase.IsStun)
        {
            Stun();
        }
    }
    public override TaskStatus OnUpdate()
    {
        if (_enemyBase.IsDie)
        {
            Die();
            return TaskStatus.Running;
        }
        if (_enemyBase.IsStun == false) return TaskStatus.Failure;
        if (AnimationEnd("Stun_End", 0.7f) == true)
        {
            _enemyBase.AnimatorCompo.SetBool("isStun", false);
            _enemyBase.IsStun = false;
            _enemyBase.CanRotate = true;
            return TaskStatus.Success;
        }
        if (_stunTime <= 0)
        {
            _enemyBase.AnimatorCompo.SetBool("Stunning", false);
        }
        else
        {
            _stunTime -= Time.deltaTime;
        }
        return TaskStatus.Running;
    }

    private void Die()
    {
        _enemyBase.CanRotate = false;
        _enemyBase.AnimatorCompo.CrossFadeInFixedTime("Die", 0.2f);
        _enemyBase.NavMeshAgentCompo.enabled = false;
        _enemyBase.HealthCompo.enabled = false;
        _enemyBase.BehaviorTreeCompo.enabled = false;
    }
    
    private void Stun()
    {
        _enemyBase.CanRotate = false;
        _enemyBase.LookPlayer();
        _stunTime = _enemyBase.StunTime;
        _enemyBase.AnimatorCompo.CrossFadeInFixedTime("Stun_Start", 0.2f);
        _enemyBase.AnimatorCompo.SetBool("isStun", true);
        _enemyBase.AnimatorCompo.SetBool("Stunning", true);
    }
}