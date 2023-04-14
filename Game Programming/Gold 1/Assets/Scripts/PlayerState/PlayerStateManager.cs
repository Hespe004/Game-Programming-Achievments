using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            if (newState.GetType() != currentState.GetType())
            {
                currentState.Exit();
                currentState = newState;
                currentState.Enter();
            }
        } else {
            currentState = newState;
            currentState.Enter();
        }

        
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}