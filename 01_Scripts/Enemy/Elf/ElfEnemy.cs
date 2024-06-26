using System.Collections.Generic;
using UnityEngine;

public class ElfEnemy : Enemy
{
    [SerializeField]
    private List<WeaponSlashEffectInfo> _slashEffectInfo;
    private List<Transform> _weaponEffectTrm;

    protected override void Awake()
    {
        base.Awake();
        SetWeaponEffectTrm();
    }



    private void SetWeaponEffectTrm()
    {
        _weaponEffectTrm = new();
        for (int i = 0; i < _slashEffectInfo.Count; i++)
        {
            Transform trm = new GameObject("WeaponEffectTrm").transform;
            trm.SetParent(AnimatorCompo.transform);
            trm.localPosition = _slashEffectInfo[i].pos;
            trm.localEulerAngles = _slashEffectInfo[i].angle;
            _weaponEffectTrm.Add(trm);
        }

    }
    public void FireSlash(int idx)
    {
        if (!IsSecondStep) return;
        Transform trm = _weaponEffectTrm[idx];
        (PoolManager.Instance.Pop(ObjectPooling.PoolingType.Effect_FireSlash) as EffectPlayer).PlayEffects(
            trm.position, trm.eulerAngles);
    }

    public void FireLine()
    {
        if (!IsSecondStep) return;
        Transform trm = _weaponEffectTrm[5];
        (PoolManager.Instance.Pop(ObjectPooling.PoolingType.Effect_FireLine) as EffectPlayer).PlayEffects(
            trm.position, trm.eulerAngles);
    }

    public void Prick()
    {
        if (IsSecondStep)
        {
            Transform trm = _weaponEffectTrm[6];
            (PoolManager.Instance.Pop(ObjectPooling.PoolingType.Effect_FirePick) as EffectPlayer).PlayEffects(
                trm.position, trm.eulerAngles);
        }
        else
        {
            Transform trm = _weaponEffectTrm[6];
            (PoolManager.Instance.Pop(ObjectPooling.PoolingType.Effect_NormalPrick) as EffectPlayer).PlayEffects(
                trm.position, trm.eulerAngles);
        }
    }
}
