using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimerController : MonoBehaviour
{

    private float timeRemaining;
    private bool timerIsRunning = false;

    public bool getTimerIsRunning()
    {
        return timerIsRunning;
    }

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timeRemaining = 120f; //how many seconds you start of with
        //Debug.Log("Time is starting!" + string.Format("{0:N2}", timeRemaining));

    }

    void Update()
    {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; //lower the amount of remaining time by the amount of time that has passed
                Debug.Log("Time is going" + string.Format("{0:N2}", timeRemaining));
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                string zero = ""; //might add a zero to format the time
                if (seconds < 10)
                {
                    zero = "0";
                }

                textmeshPro.SetText(minutes.ToString() + ":" + zero + seconds.ToString()); //display the timer
            }
            else
            {
                //If the time has run out, set the time to 0, and timerIsRunning to false
                //Debug.Log("Time has run out !");
                timeRemaining = 0;
                textmeshPro.SetText("0:00");
                timerIsRunning = false;
            }
        }
    }
}
