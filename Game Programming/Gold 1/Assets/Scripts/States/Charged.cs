using System;
using UnityEngine;

public class Charged : BaseState
{
    public static bool isCharged;
    protected float chargedRunSpeed = 2;

    public Charged(PlayerMovement stateMachine) : base("Charged", stateMachine) { 
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
        if (!_horizontalInputPressed && !_verticalInputPressed)
            stateMachine.ChangeState(player.idleState);
        if (!isCharged)
            stateMachine.ChangeState(player.movingState);
        
        //Set movement direction and normalize
        Vector3 movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
        movementDirection.Normalize();

        //Move player with an added charged run speed
        player.transform.Translate(movementDirection * (player.speed*chargedRunSpeed) * Time.deltaTime, Space.World);

        //Rotate player to face movement direction
        if (movementDirection != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, player.RotationSpeed * Time.deltaTime);
        }

    }
}
