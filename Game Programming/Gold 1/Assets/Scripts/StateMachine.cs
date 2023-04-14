using UnityEngine;
using UnityEngine.UI;

//Generic State Machine class
public class StateMachine : MonoBehaviour
{
    //Base state of the machine
    static BaseState currentState;

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    //Frame rate independent update
    void FixedUpdate()
    {
        if(currentState != null)
            currentState.Update();
    }

    //Swap the current state of the machine
    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    //Returns the current state to show on UI
    public static string GetState() {
        return currentState != null ? currentState.name : "(no current state)";
    }
}
