using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAttackState : EnemyState
{
    private BossEnemy enemy;
    public BossEnemyAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, BossEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        enemy.lastTimeAttacked = Time.time;

        enemy.MoveToPlayer();

    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            if (enemy.CanTeleport())
                stateMachine.ChangeState(enemy.TeleportState);
            else
                stateMachine.ChangeState(enemy.BattleState);
        }
    }
}
