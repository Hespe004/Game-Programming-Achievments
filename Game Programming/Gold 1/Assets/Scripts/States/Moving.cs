using UnityEngine;

public class Moving : BaseState
{
    public Moving(PlayerMovement stateMachine) : base("Moving", stateMachine) { 
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
        if (Charged.isCharged)
            stateMachine.ChangeState(player.chargedState);
        
        //2d movement
        Vector3 movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
        movementDirection.Normalize();

        player.transform.Translate(movementDirection * player.speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRotation, player.RotationSpeed * Time.deltaTime);
        }

    }
}
