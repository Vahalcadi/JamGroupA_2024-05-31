using UnityEngine;

public class TankEnemyBattleState : EnemyState
{
    private Transform player;
    private TankEnemy enemy;

    public TankEnemyBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, TankEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        stateTimer = enemy.battleTime;
        if ((new Vector2(player.transform.position.x, player.transform.position.z) - new Vector2(enemy.transform.position.x, enemy.transform.position.z)).magnitude < enemy.attackDistance)
        {

            if (CanAttack())
                stateMachine.ChangeState(enemy.AttackState);

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
