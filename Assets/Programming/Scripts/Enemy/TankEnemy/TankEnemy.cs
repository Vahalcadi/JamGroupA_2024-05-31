using UnityEngine;

public class TankEnemy : Enemy
{
    public float idleTime = 2;
    public float battleTime = 7;
    public TankEnemyIdleState IdleState { get; private set; }
    public TankEnemyBattleState BattleState { get; private set; }
    public TankEnemyAttackState AttackState { get; private set; }

    public TankEnemyDamagedState DamagedState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new TankEnemyIdleState(this, stateMachine, "Idle", this);
        BattleState = new TankEnemyBattleState(this, stateMachine, "Move", this);
        AttackState = new TankEnemyAttackState(this, stateMachine, "Attack", this);
        DamagedState = new TankEnemyDamagedState(this, stateMachine, "Damaged", this);
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
