using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public string CurrentColor { get; private set; }
    public int AmmountCollected { get; private set;}

    public void OrbCollected(string color) {
        CurrentColor = color;
        AmmountCollected++;
        Debug.Log("Current color = " + CurrentColor);
        Debug.Log("Ammount collected = " + AmmountCollected);
    }
}
