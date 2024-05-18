using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    float inputMagnitude;
    Quaternion rotation;
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        rotation = Quaternion.LookRotation(player.Input.AdjustToIsometricPlane(), Vector3.up);
        inputMagnitude = player.Input.normalized.magnitude;
        stateTimer = player.dashDuration;

        player.MakeInvincible(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();
        

        if (inputMagnitude == 0)
        {
            rb.velocity = player.transform.forward * player.dashSpeed;
        }
        else
        {          
            rb.velocity = player.transform.forward * inputMagnitude * player.dashSpeed;
        }


        if (stateTimer < 0)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //rb.MovePosition(player.transform.position + (player.transform.forward * player.Input.normalized.magnitude) * player.dashSpeed * Time.deltaTime);
    }
}
