using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    Quaternion rotation;
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        if (stateTimer > 0)
            return;

        Debug.Log(player.Input.normalized.magnitude);

        rotation = Quaternion.LookRotation(player.Input.AdjustToIsometricPlane(), Vector3.up);

        player.dashUses--;
        HUDManager.Instance.SetCooldownOf(HUDManager.Instance.dashImages[player.dashUses]);
        stateTimer = player.dashDuration;

        player.GetComponent<CapsuleCollider>().excludeLayers = player.maskToExclude;
        player.MakeInvincible(true);
    }

    public override void Exit()
    {

        base.Exit();
        player.GetComponent<CapsuleCollider>().excludeLayers = player.defaultExclude;

        player.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();
        

        if (player.Input.normalized == Vector3.zero)
        {
            rb.velocity = player.transform.forward * player.dashSpeed;
        }
        else
        {          
            rb.velocity = player.Input.AdjustToIsometricPlane() * player.dashSpeed;
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
