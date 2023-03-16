using UnityEngine;

public class Moving : BaseState
{
    private PlayerMovement _sm;

    public Moving(PlayerMovement stateMachine) : base("Moving", stateMachine) { 
        _sm = (PlayerMovement)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //Logic for switching states
        // if ((_horizontalInputPressed || _verticalInputPressed) && _runningInputPressed)
        //     stateMachine.ChangeState(_sm.runningState);
        if (!_horizontalInputPressed && !_verticalInputPressed)
            stateMachine.ChangeState(_sm.idleState);
        
        //2d movement
        Vector3 movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
        movementDirection.Normalize();

        _sm.transform.Translate(movementDirection * _sm.speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            _sm.transform.rotation = Quaternion.RotateTowards(_sm.transform.rotation, toRotation, _sm.RotationSpeed * Time.deltaTime);
        }

    }
}
