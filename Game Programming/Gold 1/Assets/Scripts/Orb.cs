using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class Orb : MonoBehaviour
{
   //List the colors the orbs can have
    public enum Colors{
        Red, Blue, Green, None
    }
    
    //Sets the color of the orb
    [Header("Orb shit")]
    [SerializeField]
    [Tooltip("Use this to set the color of the orb")]
    private Colors color;

    private void OnTriggerEnter(Collider other) {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null) {
            Charged.isCharged = true;
            playerInventory.OrbCollected(color);
            
            gameObject.SetActive(false);
            Timer.TimerOn=true;
            Timer.targetOrb=gameObject;
        }
    }
}
