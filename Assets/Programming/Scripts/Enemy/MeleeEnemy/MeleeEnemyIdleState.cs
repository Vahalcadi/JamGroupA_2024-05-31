using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyIdleState : MeleeEnemyGroundedState
{
    public MeleeEnemyIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, MeleeEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("2. IdleState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        /*if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }*/
    }
}
