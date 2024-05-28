using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyDamagedState : EnemyState
{
    private TankEnemy enemy;
    public TankEnemyDamagedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, TankEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.damageTakenEffect.Play();
    }

    public override void Exit()
    {
        base.Exit();
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
