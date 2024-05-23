using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyTeleportState : EnemyState
{
    private BossEnemy enemy;

    public BossEnemyTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, BossEnemy enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.MakeInvincible(true);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if (enemy.CanShoot())
                stateMachine.ChangeState(enemy.ShootState);
            else
            stateMachine.ChangeState(enemy.BattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.MakeInvincible(false);
    }
}
