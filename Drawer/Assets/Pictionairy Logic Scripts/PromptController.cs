using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = System.Random;

public class PromptController : MonoBehaviour
{
    private string[] promptList = {
        "Building a sandcastle on a beach",
        "Building a sandcastle on a beach",
        "Building a snowman in the snow",
        "Playing twister",
        "Playing hide and seek",
        "Playing Curling",
        "Playing icehockey",
        "Windsurfing",
        "Wakebording",
        "Bobsleding",
        "Synchornized swimming",
        "Baking christmas cookies",
        "Baking lasagna"
    };

    void Start()
    {
        //show the first prompt
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        Random rand = new System.Random();
        int index = rand.Next(promptList.Length);
        textmeshPro.SetText(promptList[index]);
    }


    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>(); //get the text component

        GameObject theTimer = GameObject.Find("Timer");
        TimerController timerScript = theTimer.GetComponent<TimerController>(); //get the timer script from the object
        bool isRunning = timerScript.getTimerIsRunning(); //see if the time is currently running

        if (Input.GetKeyDown("tab") && isRunning)
        {
            //show a new prompt
            Random rand = new System.Random();
            int index = rand.Next(promptList.Length);
            textmeshPro.SetText(promptList[index]);

        }

        if (!isRunning)
        {
            //if the time is up, remove the prompt
            textmeshPro.SetText("");
        }
    }
}



