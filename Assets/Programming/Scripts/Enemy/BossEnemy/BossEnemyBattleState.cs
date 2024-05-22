using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBattleState : EnemyState
{
    private Transform player;
    private BossEnemy enemy;

    public BossEnemyBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, BossEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.Instance.Player.transform;

        //if (player.GetComponent<PlayerStats>().isDead) 
        //    stateMachine.ChangeState(enemy.moveState);


    }

    public override void Update()
    {
        base.Update();

        stateTimer = enemy.battleTime;

        if ((player.position - enemy.transform.position).magnitude < enemy.attackDistance)
        {
            if (CanAttack())
                stateMachine.ChangeState(enemy.AttackState);
            else
                stateMachine.ChangeState(enemy.IdleState);
        }
        else
            enemy.MoveToPlayer();
    }

    public override void Exit()
    {
        base.Exit();


    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {

            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);

            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
