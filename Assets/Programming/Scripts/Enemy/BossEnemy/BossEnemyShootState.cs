using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyShootState : EnemyState
{
    private Transform player;

    private BossEnemy enemy;
    private float shootTimer;
    private float shootStateTimer;

    public BossEnemyShootState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, BossEnemy enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.Instance.Player.transform;

        enemy.transform.LookAt(player);

        enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.eulerAngles.y, 0);
        shootTimer = .1f;
        shootStateTimer = 3;
    }

    public override void Update()
    {
        base.Update();

        shootTimer -= Time.deltaTime;
        shootStateTimer -= Time.deltaTime;

        if(CanCast())
            enemy.FireProjectiles();


        if (shootStateTimer <= 0)
            stateMachine.ChangeState(enemy.TeleportState);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeShoot = Time.time;
    }

    private bool CanCast()
    {
        if (shootTimer < 0)
        {
            shootTimer = enemy.shootCooldown;
            return true;
        }

        return false;

    }
}
