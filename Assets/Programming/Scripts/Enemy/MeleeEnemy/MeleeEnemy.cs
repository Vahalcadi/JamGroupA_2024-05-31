using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public MeleeEnemyIdleState IdleState { get; private set; }
    public MeleeEnemyMoveState MoveState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new MeleeEnemyIdleState(this, stateMachine, "Idle", this);
        MoveState = new MeleeEnemyMoveState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }

    //add override Die()
}
