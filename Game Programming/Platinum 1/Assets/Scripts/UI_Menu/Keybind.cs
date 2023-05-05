using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Keybind : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private TextMeshProUGUI buttonLbl;

    // Start is called before the first frame update
    void Start()
    {
        buttonLbl.text = PlayerPrefs.GetString("CustomKey");
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonLbl.text == "Awaiting Input") {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode)) {
                    buttonLbl.text = keyCode.ToString();
                    PlayerPrefs.SetString("CustomKey", keyCode.ToString());
                }
            }
        }
    }

    public void ChangeKey() {
        buttonLbl.text = "Awaiting Input";
    }
}
