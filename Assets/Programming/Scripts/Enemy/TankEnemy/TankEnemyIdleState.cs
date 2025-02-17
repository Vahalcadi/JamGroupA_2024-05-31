public class TankEnemyIdleState : TankEnemyGroundedState
{
    public TankEnemyIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, TankEnemy _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("2. IdleState");
        stateTimer = enemy.idleTime;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        /*if (stateTimer < 0)
            stateMachine.ChangeState(enemy.BattleState);*/
    }
}
