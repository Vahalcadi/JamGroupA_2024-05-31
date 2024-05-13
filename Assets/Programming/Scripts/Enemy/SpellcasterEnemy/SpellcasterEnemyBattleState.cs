using UnityEngine;

public class SpellcasterEnemyBattleState : EnemyState
{
    private Transform player;
    private SpellcasterEnemy enemy;

    public SpellcasterEnemyBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SpellcasterEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.Instance.Player.transform;

        /*if (player.GetComponent<Player>().isDead)
            stateMachine.ChangeState(enemy.moveState);*/

        //AudioManager.instance.PlaySFX(49, enemy.transform);

    }

    public override void Update()
    {
        base.Update();

        /*if ((player.position - enemy.transform.position).magnitude < enemy.attackDistance)
        {
            if (CanAttack())
                stateMachine.ChangeState(enemy.AttackState);

        }*/

        if (CanAttack())
            stateMachine.ChangeState(enemy.AttackState);
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
