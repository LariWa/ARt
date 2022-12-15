using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//set score to 0

public class RestartController : MonoBehaviour
{
    
    private int stuffInBox = 0;
    
    [SerializeField] private GameObject eraser;
    private Vector3 eraserPosition;

    [SerializeField] private GameObject highlighter;
    private Vector3 highlighterPosition;

    [SerializeField] private GameObject brush;
    private Vector3 brushPosition;

    [SerializeField] private GameObject pencil;
    private Vector3 pencilPosition;

    [SerializeField] private GameObject palette;
    private Vector3 palettePosition;

   // private float timeInsideBox = 0.0f;

   
    // Start is called before the first frame update
    void Start()
    {
        //get all object positions
        eraserPosition = eraser.transform.position;
        highlighterPosition = highlighter.transform.position;
        brushPosition = brush.transform.position;
        pencilPosition = pencil.transform.position;
        palettePosition = palette.transform.position;

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

            //erase drawings
            var drawings = GameObject.FindGameObjectsWithTag("drawing");
            foreach (GameObject drawing in drawings)
            {
                Destroy(drawing);
            }
            //reposition objects
            eraser.transform.position = eraserPosition;
            highlighter.transform.position = highlighterPosition;
            brush.transform.position = brushPosition;
            pencil.transform.position = pencilPosition;
            palette.transform.position = palettePosition;
        }
        
        Debug.Log("ontriggerExit called ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}