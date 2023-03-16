using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    private string color;

    private void OnTriggerEnter(Collider other) {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        Debug.Log(other);

        if (playerInventory != null) {
            playerInventory.OrbCollected(color);
            gameObject.SetActive(false);
        }
    }
}
