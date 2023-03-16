using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}

public class IdleState : IState
{
    public void Enter()
    {
        Debug.Log("START IDLE");
    }
 
    public void Execute()
    {
        // Debug.Log("EXECUTE IDLE");
    }
 
    public void Exit()
    {
        Debug.Log("EXIT IDLE");
    }
}

public class MovingState : IState
{
    public void Enter()
    {
        Debug.Log("START MOVING");
    }
 
    public void Execute()
    {
        // Debug.Log("EXECUTE MOVING");
    }
 
    public void Exit()
    {
        Debug.Log("EXIT MOVING");
    }
}

public class JumpingState : IState
{
    public void Enter()
    {
        Debug.Log("START JUMPING");
    }
 
    public void Execute()
    {
        // Debug.Log("EXECUTE JUMPING");
    }
 
    public void Exit()
    {
        Debug.Log("EXIT JUMPING");
    }
}
