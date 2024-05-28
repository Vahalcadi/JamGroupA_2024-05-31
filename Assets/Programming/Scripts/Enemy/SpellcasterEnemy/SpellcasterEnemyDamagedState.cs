using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcasterEnemyDamagedState : EnemyState
{
    private SpellcasterEnemy enemy;
    public SpellcasterEnemyDamagedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SpellcasterEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
