using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody rb;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        player.weaponAnim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        player.Input = new Vector3(player.InputManager.Movement().normalized.x, 0, player.InputManager.Movement().normalized.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        player.weaponAnim.SetBool(animBoolName, false);
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
