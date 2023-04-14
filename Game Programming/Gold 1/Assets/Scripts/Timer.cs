
﻿using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float TimeLeft = 7;
    public static bool TimerOn = false;

    public Text TimerTxt;

    public static GameObject targetOrb;
   
    void Start()
    {
        //TimerOn = true;
    }

    void Update()
    {
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
                TimerTxt.text = "";

                //Reset player and orb
                targetOrb.SetActive(true);
                Charged.isCharged=false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = Color.yellow;
                PlayerInventory.CurrentPlayerColor = Orb.Colors.None;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = "Charged time remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}