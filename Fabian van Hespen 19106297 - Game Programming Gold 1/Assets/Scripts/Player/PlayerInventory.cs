using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static Orb.Colors CurrentPlayerColor = Orb.Colors.None;
    public int AmmountCollected { get; private set;}

    public void OrbCollected(Orb.Colors color) {
        AmmountCollected++;
        //GetComponent<Renderer>().material.color = color;
        switch (color)
        {
            case Orb.Colors.Red:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                CurrentPlayerColor = Orb.Colors.Red;
            break;
            case Orb.Colors.Blue:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                CurrentPlayerColor = Orb.Colors.Blue;
            break;
            case Orb.Colors.None:
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                CurrentPlayerColor = Orb.Colors.None;
            break;
            case Orb.Colors.Green:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                CurrentPlayerColor = Orb.Colors.Green;
            break;
        }
    }
}
