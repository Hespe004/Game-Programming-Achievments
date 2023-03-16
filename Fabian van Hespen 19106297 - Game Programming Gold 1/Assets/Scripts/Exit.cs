using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    Orb.Colors ExitColor = Orb.Colors.Red;

    // Start is called before the first frame update
    void Start()
    {
        ExitColor = (Orb.Colors)Random.Range(0,3);

        switch (ExitColor)
        {
            case Orb.Colors.Red:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            break;
            case Orb.Colors.None:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            break;
            case Orb.Colors.Blue:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
            break;
            case Orb.Colors.Green:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(ExitColor==PlayerInventory.CurrentPlayerColor) {
            SceneManager.LoadScene("Main Menu");
        }
     }
}
