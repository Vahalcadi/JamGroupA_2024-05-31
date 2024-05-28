using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    public bool IsDamaged { get; set; }

    [Header("VFX")]
    public ParticleSystem damageTakenEffect;

    [Header("Stunned info")]
    public float stunCooldown = 1;
    protected float stunCooldownTimer;


    [Header("Attack info")]
    public float attackCooldown;
    public float attackDistance = 2;
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
        stateMachine.CurrentState.Update();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        IsDamaged = true;
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
    public virtual void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();

    public virtual void AnimationAttackTrigger() { }

    public virtual void AssignLastAnimName(string _animBoolName)
    {
        LastAnimBoolName = _animBoolName;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + (transform.rotation * new Vector3(0, 0, attackDistance)));
    }
}
