using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int currentScore = 0;

    // Update is called once per frame
    void Update()
    {
        //check if the there is still time
        GameObject theTimer = GameObject.Find("Timer");
        TimerController timerScript = theTimer.GetComponent<TimerController>();
        bool isRunning = timerScript.getTimerIsRunning();


        //increase the score
        if (Input.GetKeyDown("tab") && isRunning)
        {
            TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
            currentScore++;
            textmeshPro.SetText(currentScore.ToString());
        }




    }
}

