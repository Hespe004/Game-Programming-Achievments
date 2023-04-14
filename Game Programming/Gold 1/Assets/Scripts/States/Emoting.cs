using System.Collections;
using UnityEngine;

public class Emoting : BaseState
{
   //List the colors the orbs can have
    public enum Colors{
        One,
        Two,
        Three,
        Four
    }
    
    //Sets the color of the orb
    private Colors EmoteColor;

    public Emoting(PlayerMovement stateMachine) : base("Emoting", stateMachine) { 
        player = (PlayerMovement)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        Emote();

        stateMachine.ChangeState(player.idleState);
        PlayerMovement._emotingInputPressed=false;
    }

    public void Emote() {

        EmoteColor = (Colors)Random.Range(0,4);

        switch (EmoteColor)
        {
            case Colors.One:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = Color.magenta;
            break;
            case Colors.Two:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = Color.cyan;
            break;
            case Colors.Three:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = Color.gray;
            break;
            case Colors.Four:
                GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = Color.clear;
            break;
        }
    }
}
