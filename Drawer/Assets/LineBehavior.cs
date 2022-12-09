using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;



public class LineBehavior : MonoBehaviour, IMixedRealityPointerHandler
{
    
    void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        
    }

    void IMixedRealityPointerHandler.OnPointerDown(
       MixedRealityPointerEventData eventData)
    {   
        if (eventData.InputSource.SourceName == "Left Hand" || eventData.InputSource.SourceName == "Mixed Reality Controller Left"){
                
                Destroy (gameObject);

            }
        
    }

    void IMixedRealityPointerHandler.OnPointerDragged(
        MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
       
    }

  // Detecting the air tap gesture
    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
    {
            
    }
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





}
