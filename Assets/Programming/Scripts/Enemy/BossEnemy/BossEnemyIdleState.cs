using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyIdleState : EnemyState
{
    private BossEnemy enemy;

    private Transform player;

    public BossEnemyIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, BossEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        player = PlayerManager.Instance.Player.transform;
    }

    public override void Exit()
    {
        base.Exit();


    }

    public override void Update()
    {
        base.Update();

        enemy.bossFightBegun = true;

        //if (Input.GetKeyDown(KeyCode.V))
        //    stateMachine.ChangeState(enemy.teleportState);

        if (stateTimer < 0 && enemy.bossFightBegun)
            stateMachine.ChangeState(enemy.BattleState);


    }
}
