using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttackState : EnemyState
{
    private MeleeEnemy enemy;
    public MeleeEnemyAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, MeleeEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
        enemy.IsDamaged = false;

    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.BattleState);
    }
}
