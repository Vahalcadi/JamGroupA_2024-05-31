using UnityEngine;

public class SpellcasterEnemy : Enemy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPosition;
    public SpellcasterEnemyIdleState IdleState { get; private set; }
    public SpellcasterEnemyBattleState BattleState { get; private set; }
    public SpellcasterEnemyAttackState AttackState { get; private set; }
    public SpellcasterEnemyDamagedState DamagedState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        IdleState = new SpellcasterEnemyIdleState(this, stateMachine, "Idle", this);
        BattleState = new SpellcasterEnemyBattleState(this, stateMachine, "Move", this);
        AttackState = new SpellcasterEnemyAttackState(this, stateMachine, "Attack", this);
        DamagedState = new SpellcasterEnemyDamagedState(this, stateMachine, "Damaged", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();

        var projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, projectileSpawnPosition.rotation);
        projectile.GetComponent<Projectile>().Damage = Damage;
    }
}
