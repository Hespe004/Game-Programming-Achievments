using UnityEngine;

//Generic state class
public class BaseState
{
    public string name;
    protected StateMachine stateMachine;
    protected PlayerMovement player;

    //Used to see if button is pressed
    protected float _horizontalInput;
    protected float _verticalInput;

    protected bool _horizontalInputPressed;
    protected bool _verticalInputPressed;
    protected bool _emotingInputPressed;

    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() 
    {
        _horizontalInput = 0f;
        _verticalInput = 0f;
    }

    //Update runs on FixedUpdate (see StateMachine.cs)
    public virtual void Update() 
    {
        //Get input from player
        _horizontalInput = Input.GetAxis("Horizontal"); //"Horizontal" is bound to A,D
        _verticalInput = Input.GetAxis("Vertical"); //"Vertical" is bound to W,S

        //check if button is pressed
        _horizontalInputPressed = Mathf.Abs(_horizontalInput) > Mathf.Epsilon;
        _verticalInputPressed = Mathf.Abs(_verticalInput) > Mathf.Epsilon;
        _emotingInputPressed = Input.GetKeyDown(KeyCode.E);
    }

    public virtual void Exit() { }
}
