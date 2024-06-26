using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ConditionalBase : Conditional
{
    protected Enemy _enemyBase;
    public override void OnStart()
    {
        base.OnStart();
        _enemyBase = GetComponent<Enemy>();
    }
}