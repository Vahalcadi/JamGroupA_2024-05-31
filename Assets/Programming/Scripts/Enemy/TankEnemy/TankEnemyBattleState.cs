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

        enemy.transform.LookAt(player.transform);
        /*if (player.GetComponent<Player>().isDead)
            stateMachine.ChangeState(enemy.moveState);*/

        //AudioManager.instance.PlaySFX(49, enemy.transform);

    }

    public override void Update()
    {
        base.Update();

        if (CanAttack())
        {
            rb.velocity = enemy.transform.forward * enemy.Speed;
            // Ray ray = new Ray(enemy.transform.position, enemy.transform.position + (enemy.transform.rotation * new Vector3(0, 0, enemy.attackDistance)));

            if ((player.position - enemy.transform.position).magnitude < enemy.attackDistance)
                stateMachine.ChangeState(enemy.AttackState);
            else if (Physics.Raycast(enemy.transform.position, enemy.transform.rotation * Vector3.forward, enemy.attackDistance, enemy.obstructionMask))
                stateMachine.ChangeState(enemy.AttackState);



        }

        /*if ((player.position - enemy.transform.position).magnitude < enemy.attackDistance)
        {
            if (CanAttack())
                stateMachine.ChangeState(enemy.AttackState);

        }
        else
            enemy.MoveToPlayer();*/
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
