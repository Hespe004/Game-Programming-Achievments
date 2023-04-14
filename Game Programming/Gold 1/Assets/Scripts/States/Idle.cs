using System.Collections;
using UnityEngine;

public class Idle : BaseState
{
    public Idle(PlayerMovement stateMachine) : base("Idle", stateMachine) { 
        player = (PlayerMovement)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //Logic for switching states
        if (_horizontalInputPressed || _verticalInputPressed) {
            stateMachine.ChangeState(player.movingState);
        }
        else if (PlayerMovement._emotingInputPressed) {
            stateMachine.ChangeState(player.emoteState);
        }
    }
}
