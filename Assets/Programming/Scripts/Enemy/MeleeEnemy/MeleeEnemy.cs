using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public MeleeEnemyIdleState IdleState { get; private set; }
    public MeleeEnemyBattleState BattleState { get; private set; }
    public MeleeEnemyAttackState AttackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new MeleeEnemyIdleState(this, stateMachine, "Idle", this);
        BattleState = new MeleeEnemyBattleState(this, stateMachine, "Move", this);
        AttackState = new MeleeEnemyAttackState(this, stateMachine, "Attack", this);

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
