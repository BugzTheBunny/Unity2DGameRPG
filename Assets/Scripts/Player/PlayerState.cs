using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine; 
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        Debug.Log("Entered State : " + animBoolName);
    }

    public virtual void Update()
    {
        Debug.Log("Current State : " + animBoolName);
    }


    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }

}
