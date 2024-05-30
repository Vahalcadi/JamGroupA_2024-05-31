using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyDeadState : EnemyState
{
    private BossEnemy enemy;
    public BossEnemyDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, BossEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.chanceToTeleport += 5;
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.EndGame("You Won! Play again?");

    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
}
