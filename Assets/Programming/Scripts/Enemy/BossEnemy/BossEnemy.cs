
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public BoxCollider cd;
    public LayerMask whatIsGround;
    public GameObject projectile;
    public List<Transform> projectileSpawners;
    public float shootCooldown;
    [HideInInspector] public float lastTimeShoot;
    public int projectileDamage;
    public float shootStateCooldown;
    [HideInInspector] public float shootStateCooldownTimer;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider arena;
    [SerializeField] private Vector3 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;
    public bool bossFightBegun;
    public float idleTime = 2;
    public float battleTime = 7;

    public BossEnemyIdleState IdleState { get; private set; }
    public BossEnemyBattleState BattleState { get; private set; }
    public BossEnemyAttackState AttackState { get; private set; }
    //public DeathBringerDeadState deadState { get; private set; }
    public BossEnemyShootState ShootState { get; private set; }
    public BossEnemyTeleportState TeleportState { get; private set; }
    public BossEnemyDeadState DeadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new BossEnemyIdleState(this, stateMachine, "Idle", this);
        BattleState = new BossEnemyBattleState(this, stateMachine, "Move", this);
        AttackState = new BossEnemyAttackState(this, stateMachine, "Attack", this);
        ShootState = new BossEnemyShootState(this, stateMachine, "Shoot", this);
        TeleportState = new BossEnemyTeleportState(this, stateMachine, "Teleport", this);
        DeadState = new BossEnemyDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }


    public override void Die()
    {
        base.Die();
        //stateMachine.ChangeState(deadState);

    }

    public override void TakeDamage(int damage)
    {
        CurrentHP = Mathf.Clamp(CurrentHP - damage, 0, MaxHealth);

        StartCoroutine(HitKnockback());

        if (CurrentHP <= 0)
            stateMachine.ChangeState(DeadState);
    }

    public void FireProjectiles()
    {
        foreach (Transform ps in projectileSpawners)
        {
            GameObject projectile = Instantiate(this.projectile, ps.position, Quaternion.Euler(this.projectile.transform.rotation.eulerAngles.x, ps.rotation.eulerAngles.y, this.projectile.transform.rotation.eulerAngles.z));
            var projectileRotation = projectile.transform.eulerAngles;
            projectile.transform.LookAt(PlayerManager.Instance.Player.transform.position);
            projectile.transform.rotation = Quaternion.Euler(projectile.transform.rotation.eulerAngles.x - 5, projectileRotation.y, projectileRotation.z);
            projectile.GetComponent<Projectile>().Damage = projectileDamage;
        }

    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.z + 3, arena.bounds.max.z - 3);

        transform.position = new Vector3(x, transform.position.y, y);

        if (!GroundBelow() || SomethingIsAround())
        {
            Debug.Log("looking for new position");
            FindPosition();
        }
    }

    private bool GroundBelow() => Physics.Raycast(transform.position, transform.rotation * Vector3.down, 100, whatIsGround);
    //private bool SomethingIsAround() => Physics.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);
    private bool SomethingIsAround() => Physics.BoxCast(transform.position, surroundingCheckSize, Vector3.zero, Quaternion.identity, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }


        return false;
    }

    public bool CanShoot()
    {
        if (Time.time >= lastTimeShoot + shootCooldown)
        {
            return true;
        }

        return false;
    }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();
    }
}
