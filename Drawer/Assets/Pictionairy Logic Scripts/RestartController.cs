using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//set score to 0

public class RestartController : MonoBehaviour
{
    
    private int stuffInBox = 0;
   // private float timeInsideBox = 0.0f;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void OnTriggerEnter(Collider collision) 
    {
        Debug.Log("ontriggerEnter called ");
        stuffInBox ++;
    
    }

    void OnTriggerExit(Collider collision) 
    {
        stuffInBox --;
        if(stuffInBox == 0){
            GameObject theTimer = GameObject.Find("Timer");
            TimerController timerScript = theTimer.GetComponent<TimerController>(); //get the timer script from the object
            timerScript.setTimeRemaining(120.0f);
            timerScript.setIsRunning(true);

            GameObject theScore = GameObject.Find("Score");
            ScoreController scoreScript = theScore.GetComponent<ScoreController>();
            scoreScript.resetScore();

           
        }
        
        Debug.Log("ontriggerExit called ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}