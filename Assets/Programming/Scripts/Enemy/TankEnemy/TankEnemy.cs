using UnityEngine;

public class TankEnemy : Enemy
{
    public LayerMask obstructionMask;
    public float jumpDuration;
    public float jumpAcceleration;
    public TankEnemyIdleState IdleState { get; private set; }
    public TankEnemyBattleState BattleState { get; private set; }
    public TankEnemyAttackState AttackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new TankEnemyIdleState(this, stateMachine, "Idle", this);
        BattleState = new TankEnemyBattleState(this, stateMachine, "Move", this);
        AttackState = new TankEnemyAttackState(this, stateMachine, "Attack", this);

    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }

    public override void AnimationAttackTrigger()
    {
        base.AnimationAttackTrigger();


    }
}
