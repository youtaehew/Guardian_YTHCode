using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ActionBase : Action
{
    protected Enemy _enemyBase;
    protected bool _endTriggerCalled;
    protected int _animBoolHash;


    public override void OnAwake()
    {
        _enemyBase = GetComponent<Enemy>();
    }


    public bool AnimationEnd(string name, float ExitTime = .9f)
    {
        return _enemyBase.AnimatorCompo.GetCurrentAnimatorStateInfo(0).IsName(name) &&
            _enemyBase.AnimatorCompo.GetCurrentAnimatorStateInfo(0).normalizedTime >= ExitTime;
    }
}