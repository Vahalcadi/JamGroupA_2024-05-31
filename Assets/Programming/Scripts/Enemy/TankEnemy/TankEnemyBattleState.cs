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

        stateTimer = enemy.jumpDuration;

        enemy.transform.LookAt(player.transform);

        enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.eulerAngles.y, 0);
        /*if (player.GetComponent<Player>().isDead)
            stateMachine.ChangeState(enemy.moveState);*/

        //AudioManager.instance.PlaySFX(49, enemy.transform);

    }

    public override void Update()
    {
        base.Update();

        if (CanAttack())
        {
            //rb.velocity = enemy.transform.forward * enemy.Speed;
            //Ray ray = new Ray(enemy.transform.position, enemy.transform.position - enemy.transform.up);

            /*if ((player.position - enemy.transform.position).magnitude < enemy.attackDistance)
                stateMachine.ChangeState(enemy.AttackState);
            else if (Physics.Raycast(enemy.transform.position, enemy.transform.rotation * Vector3.forward, enemy.attackDistance, enemy.obstructionMask))
                stateMachine.ChangeState(enemy.AttackState);*/
            //Physics.Raycast(enemy.transform.position,enemy.transform.position - enemy.transform.up, Mathf.Infinity, LayerMask.NameToLayer("Player"))

            if(stateTimer < 0 || (stateTimer < enemy.jumpDuration / 2 && Physics.Raycast(enemy.transform.position, enemy.transform.rotation * Vector3.down, Mathf.Infinity, enemy.obstructionMask))) 
            {
                enemy.rb.AddForce(0, -10000, 0, ForceMode.Impulse);
                stateMachine.ChangeState(enemy.AttackState);
            }
            else
            {
                enemy.rb.AddForce(0, enemy.jumpAcceleration, 0, ForceMode.Acceleration);
                enemy.MoveToPlayer();
            }


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
