using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAnimationManager : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    [SerializeField]
    private SerializedDictionary<WeaponDamageType, int> _firstStepDamage;
    [SerializeField]
    private SerializedDictionary<WeaponDamageType, int> _secondStepDamage;

    private void Awake()
    {

        _enemy = transform.parent.GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
    }


    public void StartAttack(int a)
    {
        if (!_enemy) return;
        _enemy.CanRotate = false;
        _enemy.colliderList[a].EnableCollider(true);
    }

    public void StartAttackElf()
    {
        if (!_enemy) return;
        _enemy.CanRotate = false;
        _enemy.LookPlayer();
        _enemy.colliderList[0].EnableCollider(true);
        _enemy.colliderList[0].EnableTrailEffect(true);
    }

    public void EndAttackElf()
    {
        if (!_enemy) return;
        _enemy.CanRotate = true;
        _enemy.colliderList[0].EnableCollider(false);
        _enemy.colliderList[0].EnableTrailEffect(false);
    }

    public void BothStartAttack()
    {
        if (!_enemy) return;
        _enemy.CanRotate = false;
        _enemy.colliderList[0].EnableCollider(true);
        _enemy.colliderList[1].EnableCollider(true);
        _enemy.colliderList[0].EnableTrailEffect(false);
    }

    public void EndAttack()
    {
        if (!_enemy) return;

        _enemy.CanRotate = true;
        _enemy.RotateSpeed = 6f;

        foreach (Weapon col in _enemy.colliderList)
        {
            col.EnableCollider(false);
        }
    }

    public void MoveToPlayer()
    {
        if (!_enemy) return;

        //_enemy.MoveToPlayer();
    }

    public void JumpStart()
    {
        if (!_enemy) return;
        _enemy.CanJump = true;
    }

    public void JumpEnd()
    {
        if (!_enemy) return;
        _enemy.CanJump = false;
    }


    private void OnAnimatorMove()
    {
        Vector3 targetPos = _animator.rootPosition;
        if (!_enemy)
        {
            transform.position = targetPos;
            return;
        }

        targetPos.y = _enemy.NavMeshAgentCompo.nextPosition.y;
        _enemy.transform.position = targetPos;
        _enemy.NavMeshAgentCompo.nextPosition = targetPos;
    }

    private void EnableLegsAnimator()
    {
        if (!_enemy) return;
    }

    private void DisableLegsAnimator()
    {
        if (!_enemy) return;

        // _enemy.LegsAnimatorCompo.User_FadeToDisabled(0);
    }

    private void SetCombatData(int animationType)
    {
        if (!_enemy) return;

        _enemy.CombatData.hitAnimationType = animationType;
    }

    private void SpawnGroundCrackParticle()
    {
        if (!_enemy) return;

        EffectPlayer player = (PoolManager.Instance.Pop(ObjectPooling.PoolingType.Effect_GroundCrack) as EffectPlayer);
        player.transform.position = _enemy.transform.forward + _enemy.transform.position + new Vector3(0, 0.1f, 0);
        player.PlayEffects();
    }

    private void SpawnSnapWaveVFX()
    {
        if (!_enemy) return;

        EffectPlayer player = (PoolManager.Instance.Pop(ObjectPooling.PoolingType.Effect_SnapWave) as EffectPlayer);
        player.transform.position = _enemy.transform.forward + _enemy.transform.position + new Vector3(0, 0.5f, 0);
        player.PlayEffects();
    }

    private void SpawnJumpCrackParticle()
    {
        if (!_enemy) return;

        EffectPlayer player = (PoolManager.Instance.Pop(ObjectPooling.PoolingType.Effect_JumpCrack) as EffectPlayer);
        Vector3 ArrivePos = _enemy.transform.position;
        ArrivePos.y = 0.1f;
        player.transform.position = ArrivePos;
    }

    private void ChangeWeaponDamageType(int type)
    {
        if (!_enemy) return;
        WeaponDamageType weaponDamageType = (WeaponDamageType)(type);
        _enemy.WeaponDamageType = weaponDamageType;

        PlayerManager.Instance.Player.EnableImpossibleParryingIcon(weaponDamageType == WeaponDamageType.VeryHeavy);
        if (!_enemy.IsSecondStep)
        {
            for (int i = 0; i < _enemy.colliderList.Count(); i++)
            {
                (_enemy.colliderList[i] as BossWeapon)._damage = _firstStepDamage[weaponDamageType];
            }
        }
        else
        {
            for (int i = 0; i < _enemy.colliderList.Count(); i++)
            {
                (_enemy.colliderList[i] as BossWeapon)._damage = _secondStepDamage[weaponDamageType];
            }
        }
    }

    private void SpawnShardEffect(int idx)
    {
        if (!_enemy) return;

        _enemy.SpawnShardEffect(idx);
    }

    private void ImpulseEffect(float force)
    {
        if (!_enemy) return;

        _enemy.ImpulseEffect(force);
    }


}