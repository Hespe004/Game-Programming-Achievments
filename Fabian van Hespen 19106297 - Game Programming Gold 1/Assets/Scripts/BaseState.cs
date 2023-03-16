using UnityEngine;

//Generic state class
public class BaseState
{
    public string name;
    protected StateMachine stateMachine;

    //Variables for player input; found myself using these in all states, so decided to put them in the base class
    protected float _horizontalInput;
    protected float _verticalInput;
    protected float _jumpInput;
    protected float _runningInput;

    protected bool _horizontalInputPressed;
    protected bool _verticalInputPressed;
    protected bool _jumpInputPressed;
    protected bool _runningInputPressed;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() 
    {
        //Initialize variables for player input
        _horizontalInput = 0f;
        _verticalInput = 0f;
        _jumpInput = 0f;
        _runningInput = 0f;
    }

    //Update runs on FixedUpdate (see StateMachine.cs)
    public virtual void Update() 
    {
        //Get input from player
        _horizontalInput = Input.GetAxis("Horizontal"); //"Horizontal" is bound to A,D
        _verticalInput = Input.GetAxis("Vertical"); //"Vertical" is bound to W,S
        _jumpInput = Input.GetAxis("Jump"); //"Jump" is bound to space
        _runningInput = Input.GetAxis("Fire3"); //"Fire3" is bound to shift

        //Booleans to check if buttons are pressed, these are used for switching states
        _horizontalInputPressed = Mathf.Abs(_horizontalInput) > Mathf.Epsilon;
        _verticalInputPressed = Mathf.Abs(_verticalInput) > Mathf.Epsilon;
        _jumpInputPressed = Mathf.Abs(_jumpInput) > Mathf.Epsilon;
        _runningInputPressed = Mathf.Abs(_runningInput) > Mathf.Epsilon;
    }

    public virtual void Exit() { }
}
