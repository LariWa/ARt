using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class GetPos : MonoBehaviour, IMixedRealityTouchHandler
{

    float timer;
    float timerdelay = 0.02f;

    // Start is called before the first frame update

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        // string ptrName = eventData.Pointer.PointerName;
        // Debug.Log($"Touch started from {ptrName}");
        print(gameObject.transform.position);
        print(gameObject.transform.rotation);
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData) {}
    public void OnTouchUpdated(HandTrackingInputEventData eventData) { }
    void Start()
    {
        timer = timerdelay;
        
    }

    // Update is called once per frame
    void Update()
    {
        

        // timer -= Time.deltaTime;
        // if (timer <= 0){
        //     print(Camera.main.transform.position);
        //     print(Camera.main.transform.rotation);
        // }
    }
}
