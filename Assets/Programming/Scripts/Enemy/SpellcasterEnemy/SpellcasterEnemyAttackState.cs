using UnityEngine;

public class SpellcasterEnemyAttackState : EnemyState
{
    private SpellcasterEnemy enemy;
    public SpellcasterEnemyAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SpellcasterEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        enemy.transform.LookAt(PlayerManager.Instance.Player.transform);

        enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.eulerAngles.y, 0);

        if (triggerCalled)
            stateMachine.ChangeState(enemy.BattleState);
    }
}
