using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    public bool IsDamaged { get; set; }

    [Header("AI")]
    private NavMeshAgent agent;

    [Header("VFX")]
    public ParticleSystem damageTakenEffect;
    public VisualEffect damageTakenEffect1;
    public VisualEffect attackVFX;
    private Material material;
    private bool hasDamageCoroutineStarted;

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
        agent = GetComponent<NavMeshAgent>();
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
        damageTakenEffect1.Play();
        
        if(this.isActiveAndEnabled)
            StartCoroutine(ShowDamageVFX());
        IsDamaged = true;
    }

    private IEnumerator ShowDamageVFX()
    {
        if (hasDamageCoroutineStarted)
            yield return null;

        material = GetComponentInChildren<Renderer>().material;
        hasDamageCoroutineStarted = true;
        material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        material.color = Color.white;
        hasDamageCoroutineStarted = false;
    }

    public virtual void GetStunned()
    {
        stunCooldownTimer = stunCooldown;
    }

    public override void SetZeroVelocity()
    {
        base.SetZeroVelocity();
        agent.velocity = Vector3.zero;
    }

    public override void MoveToPlayer()
    {
        //base.MoveToPlayer();

        agent.SetDestination(new Vector3(PlayerManager.Instance.Player.transform.position.x, transform.position.y, PlayerManager.Instance.Player.transform.position.z));
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
