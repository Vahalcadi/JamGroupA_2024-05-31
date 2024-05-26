using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    //Ray mousePos;

    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .1f;

        //mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            /*Physics.Raycast(mousePos, out var hitInfo);
            player.transform.LookAt(new Vector3(hitInfo.point.x, 1, hitInfo.point.z), Vector3.up);
            player.transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);*/
            //player.SetZeroVelocity();
            rb.MovePosition(player.transform.position + player.transform.forward * (player.Speed /4) * Time.fixedDeltaTime);
        }

        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
