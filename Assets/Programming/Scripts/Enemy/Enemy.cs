using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stunned info")]
    public float stunCooldown = 1;
    protected float stunCooldownTimer;

    [Header("Move info")]
    public float moveSpeed = 1.5f;
    public float idleTime = 2;
    public float battleTime = 7;

    [Header("Attack info")]
    public float attackCooldown;
    public float minAttackCooldown = .35f;
    public float maxAttackCooldown = .55f;
    [HideInInspector] public float lastTimeAttacked;

    public EnemyStateMachine stateMachine { get; private set; }
    public string LastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stunCooldownTimer = Time.deltaTime;
        stateMachine.currentState.Update();
    }

    public virtual void GetStunned()
    {
        stunCooldownTimer = stunCooldown;
    }

    public virtual bool CanBeStunned()
    {
        if (stunCooldownTimer < 0)
            return true;
        return false;
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual void AnimationAttackTrigger() { }

    public virtual void AssignLastAnimName(string _animBoolName)
    {
        LastAnimBoolName = _animBoolName;
    }
}
