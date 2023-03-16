using UnityEngine;

public class PlayerMovement : StateMachine
{
    //All available states
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving movingState;

    //Variables to control moving and jumping; editable in inspector
    public float speed = 5;
    public float RotationSpeed = 720;

    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
    }

    protected override BaseState GetInitialState()
    {
        //Set this machine's base state to Idle
        return idleState;
    }
}
