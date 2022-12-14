using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int currentScore = 0;

    public void resetScore(){
        currentScore = 0;
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        textmeshPro.SetText(currentScore.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        //check if the there is still time
        GameObject theTimer = GameObject.Find("Timer");
        TimerController timerScript = theTimer.GetComponent<TimerController>();
        bool isRunning = timerScript.getTimerIsRunning();

        GameObject checkbox = GameObject.Find("green checkbox");
        CheckboxController checkboxScript = checkbox.GetComponent<CheckboxController>();
        bool checkboxIsHit = checkboxScript.getCheckboxHit();


        //increase the score
        if (checkboxIsHit && isRunning)
        {
            TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
            currentScore++;
            textmeshPro.SetText(currentScore.ToString());
            checkboxScript.setCheckboxHit(false);
        }




    }
}

