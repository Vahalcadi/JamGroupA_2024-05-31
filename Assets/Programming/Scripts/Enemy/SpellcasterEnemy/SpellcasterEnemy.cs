public class SpellcasterEnemy : Enemy
{
    public SpellcasterEnemyIdleState IdleState { get; private set; }
    public SpellcasterEnemyBattleState BattleState { get; private set; }
    public SpellcasterEnemyAttackState AttackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new SpellcasterEnemyIdleState(this, stateMachine, "Idle", this);
        BattleState = new SpellcasterEnemyBattleState(this, stateMachine, "Move", this);
        AttackState = new SpellcasterEnemyAttackState(this, stateMachine, "Attack", this);

    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }

    public override void AnimationAttackTrigger()
    {
        //base.AnimationAttackTrigger();

        //spawn a projectile
        //damage logic should be inside the projectile.
        //damage should be done when projectile collides with player
    }
}
