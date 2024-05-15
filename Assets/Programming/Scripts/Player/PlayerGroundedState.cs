using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.InputManager.Attack())
        {
            var mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(mousePos, out var hitInfo);
            player.transform.LookAt(new Vector3(hitInfo.point.x, 1, hitInfo.point.z), Vector3.up);
            player.transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);

            stateMachine.ChangeState(player.AttackState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
