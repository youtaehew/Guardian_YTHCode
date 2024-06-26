using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Cinemachine;
using DG.Tweening;
using FIMSpace.FLook;
using UnityEngine;
using UnityEngine.AI;
using INab.Dissolve;

public class Enemy : MonoBehaviour
{
    private readonly int emissionColorHash = Shader.PropertyToID("_EmissionColor");

    #region 컴포넌트

    public Animator AnimatorCompo { get; protected set; }
    public NavMeshAgent NavMeshAgentCompo { get; protected set; }
    public BehaviorTree BehaviorTreeCompo { get; protected set; }

    [SerializeField] private ParticleSystem[] _shardEffects;

    public Health HealthCompo { get; private set; }

    public EnemyAnimationManager EnemyAnimationManager { get; protected set; }

    // public LegsAnimator LegsAnimatorCompo { get; protected set; }
    public FLookAnimator LookAnimatorCompo { get; protected set; }
    protected Dissolver _dissolver;
    private CinemachineImpulseSource _impulseSource;
    private SkinnedMeshRenderer _meshRenderer;

    #endregion

    [SerializeField, ColorUsage(true, true)]
    private Color _step2EmissionColor;

    public ImpactType ImpactType;
    public GameObject Player;
    public float MoveDistance;
    public float MoveTime;

    public float DistanceDestination;
    public float DistancePlayer;

    [Header("Colliders")] public Weapon[] colliderList;

    public bool CanRotate;
    public bool CanJump = false;
    public bool StartJump = false;

    public CombatData CombatData;

    public ParticleSystem GroundCrackParticle;
    public WeaponDamageType WeaponDamageType;
    public float RotateSpeed = 3f;

    public bool IsStun = false;
    public bool IsDie = false;
    public bool IsSecondStep;
    public float StunTime = 5f;

    [Header("Step1")] public float BaseMoveSpeed;
    public float WaitTime;
    public float CombatMinTime;
    public float CombatMaxTime;

    [Header("Step2")] public float ChangeMoveSpeed;
    public float ChangeWaitTime;
    public float ChangeCombatMinTime;
    public float ChangeCombatMaxTime;
    public RuntimeAnimatorController ChangeAnimator;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        NavMeshAgentCompo = GetComponent<NavMeshAgent>();
        BehaviorTreeCompo = GetComponent<BehaviorTree>();
        EnemyAnimationManager = transform.GetChild(0).GetComponent<EnemyAnimationManager>();
        LookAnimatorCompo = visualTrm.GetComponent<FLookAnimator>();
        HealthCompo = GetComponent<Health>();
        HealthCompo.OnDeadEvent += Die;
        _dissolver = GetComponent<Dissolver>();
        _impulseSource = transform.Find("ImpulseSource").GetComponent<CinemachineImpulseSource>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        StartStep1();
    }

    private void StartStep1()
    {
        NavMeshAgentCompo.speed = BaseMoveSpeed;
        BehaviorTreeCompo.SetVariableValue("waitTIme", WaitTime);
        BehaviorTreeCompo.SetVariableValue("CombatMinTime", CombatMinTime);
        BehaviorTreeCompo.SetVariableValue("CombatMaxTime", CombatMaxTime);
    }

    public void StartStep2()
    {
        IsStun = false;
        IsSecondStep = true;
        DOTween.To(() => _meshRenderer.material.GetColor(emissionColorHash),
            x => _meshRenderer.material.SetColor(emissionColorHash, x), _step2EmissionColor, .5f);
        NavMeshAgentCompo.speed = ChangeMoveSpeed;
        BehaviorTreeCompo.SetVariableValue("waitTIme", ChangeWaitTime);
        BehaviorTreeCompo.SetVariableValue("CombatMinTime", CombatMinTime);
        BehaviorTreeCompo.SetVariableValue("CombatMaxTime", CombatMaxTime);
        AnimatorCompo.runtimeAnimatorController = ChangeAnimator;
        colliderList[0].GetComponent<BossWeapon>().ColliderBig();
    }

    private void Update()
    {
        DistanceToPlayer();
    }

    private void FixedUpdate()
    {
        if (CanRotate)
        {
            RotateToPlayer();
        }
    }


    private void DistanceToPlayer()
    {
        DistancePlayer = Vector3.Distance(Player.transform.position, transform.position);
        DistanceDestination = (NavMeshAgentCompo.destination - Player.transform.position).magnitude;
    }

    public void LookPlayer()
    {
        Vector3 dir = (Player.transform.position - transform.position).normalized;
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        transform.DORotateQuaternion(look, .1f);
    }

    private void RotateToPlayer()
    {
        Vector3 playerToDir = (Player.transform.position - transform.position).normalized;
        playerToDir.y = 0;
        transform.forward += Time.fixedDeltaTime * playerToDir * RotateSpeed;
    }


    public void SpawnShardEffect(int idx)
    {
        ParticleSystem ps = _shardEffects[idx];
        Vector3 dir = (Player.transform.position - transform.position).normalized;
        dir.y = 0;
        Quaternion look = Quaternion.LookRotation(dir);
        ps.transform.rotation = look;
        ps.GetComponentInChildren<ParticleTriggerDamageForEnemy>().Reset();
        ps.Play();
    }

    public void ModifyDamage(int damage)
    {
        for (int i = 0; i < colliderList.Length; i++)
        {
            colliderList[i].GetComponent<BossWeapon>()._damage = damage;
        }
    }

    public void ImpulseEffect(float force)
    {
        _impulseSource.GenerateImpulseAt(transform.position, Vector3.down * force);
    }

    private void Die()
    {
        IsStun = false;
        Material mat = AnimatorCompo.GetComponentInChildren<SkinnedMeshRenderer>().material;
        _dissolver.materials = new List<Material> { mat };
        _dissolver.Dissolve();
        LookAnimatorCompo.enabled = false;
        BodyPart[] bodyParts = GetComponentsInChildren<BodyPart>();
        for (int i = 0; i < bodyParts.Length; i++)
        {
            Destroy(bodyParts[i]);
        }

        Destroy(FindObjectOfType<CapsuleCollider>());
        for (int i = 0; i < colliderList.Length; i++)
        {
            Destroy(colliderList[i].gameObject);
        }

        IsDie = true;
    }
}