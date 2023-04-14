using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : StateMachine
{
    //All states are listed here
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving movingState;
    [HideInInspector]
    public Charged chargedState;
    [HideInInspector]
    public Emoting emoteState;

    [SerializeField]
    private bool isPaused = false;

    public float speed = 5;
    public float RotationSpeed = 720;
    //Used to show the current state in the UI
    public Text StateTxt;
    public static bool _emotingInputPressed;

    private void Awake()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
        chargedState = new Charged(this);
        emoteState = new Emoting(this);
    }

    protected override BaseState GetInitialState()
    {
        //Set this machine's base state to Idle
        return idleState;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = isPaused ? 1 : 0;
            isPaused = !isPaused;
        }
        else if (Input.GetKeyDown(KeyCode.E)) {
            _emotingInputPressed = true;
        }
        
        StateTxt.text = "Current state = " + StateMachine.GetState();
    }
}
