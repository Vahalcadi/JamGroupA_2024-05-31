using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyGroundedState : EnemyState
{
    protected MeleeEnemy enemy;
    protected Transform player;

    public MeleeEnemyGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, MeleeEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.Instance.Player.transform;

        //Debug.Log("1. GroundedState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        stateMachine.ChangeState(enemy.BattleState);

        /*if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < enemy.aggroDistance)
            stateMachine.ChangeState(enemy.battleState);*/
    }
}
