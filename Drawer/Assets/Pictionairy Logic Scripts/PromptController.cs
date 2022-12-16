using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = System.Random;

public class PromptController : MonoBehaviour
{
    public static PromptController instance;
    private string[] promptList = {
        //"Building a sandcastle on a beach",
        //"Building a sandcastle on a beach",
        //"Building a snowman in the snow",
        //"Playing twister",
        //"Playing hide and seek",
        //"Playing Curling",
        //"Playing icehockey",
        //"Windsurfing",
        //"Wakebording",
        //"Bobsleding",
        //"Synchornized swimming",
        //"Baking christmas cookies",
        //"Baking lasagna",
        //"Climbing a tree",
        //"Laying in bed",
        //"Swimming",
        //"Bowling",
        //"Fishing",
        //"Playing piano",
        //"Listening to music",
        //"Watching television",
        //"Typing on a keyboard"
        "spider web",
        "cupcake",
        "mouse",
        "fish",
        "snowflake",
        "glasses",
        "snail",
        "planet",
        "book",
        "diamond",
        "computer",
        "mountain",
        "hand",
        "pen",
        "feather",
        "tree",
        "jellyfish",
        "hat",
        "banana",
        "fork"
    };

    void Start()
    {
        instance = this;
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

        GameObject checkbox = GameObject.Find("green checkbox");
        CheckboxController checkboxScript = checkbox.GetComponent<CheckboxController>();
        bool checkboxIsHit = checkboxScript.getCheckboxHit();

        if (checkboxIsHit && isRunning)
        {


        }

        if (!isRunning)
        {
            //if the time is up, remove the prompt
            textmeshPro.SetText("");
        }
    }
    public void correctGuess()
    {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>(); //get the text component

        //show a new prompt
        Random rand = new System.Random();
        int index = rand.Next(promptList.Length);
        textmeshPro.SetText(promptList[index]);
        //checkboxScript.setCheckboxHit(false);
    }
}



