using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class Orb : MonoBehaviour
{
   
    public enum Colors{
        Red, Blue, Green, None
    }
    
    [Header("Orb shit")]
    [SerializeField]
    [Tooltip("Use this to set the color of the orb")]
    private Colors color;

    private void OnTriggerEnter(Collider other) {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null) {
            playerInventory.OrbCollected(color);
            gameObject.SetActive(false);
        }
    }
}
