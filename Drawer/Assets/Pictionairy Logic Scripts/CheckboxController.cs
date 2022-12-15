using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckboxController : MonoBehaviour
{
    
    private bool checkboxHit = false;
    private int stuffInBox = 0;
   // private float timeInsideBox = 0.0f;

    public bool getCheckboxHit()
    {
        return checkboxHit;
    }

    public void setCheckboxHit(bool b){
        checkboxHit = b;
    }


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
            checkboxHit = true;
        }
        
        Debug.Log("ontriggerExit called ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}