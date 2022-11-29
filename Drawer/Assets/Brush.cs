using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;


using UnityEngine;

using UnityEngine;

//, IMixedRealityTouchHandler, IMixedRealityInputHandler
public class Brush : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityTouchHandler, IMixedRealityInputHandler
{
    List<Vector3> linePoints;
    float timer;
    public float timerdelay;

    GameObject newLine;
    LineRenderer drawLine;
    public float lineWidth;

    Vector2 lastPos;
        
    private MixedRealityInputAction grabAction = MixedRealityInputAction.None;

	// void Awake()
    // {
    //     CoreServices.InputSystem?.RegisterHandler<IMixedRealityPointerHandler>(this);
    // }
	

	void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
        linePoints.Clear();
    }

    void IMixedRealityPointerHandler.OnPointerDown(
       MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
        Debug.Log(linePoints.Count);
        newLine = new GameObject();
        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.material = new Material (Shader.Find("Sprites/Default"));
        drawLine.startColor = Color.green;
        drawLine.endColor = Color.green;
        drawLine.startWidth = lineWidth;
        drawLine.endWidth = lineWidth;
    }

    void IMixedRealityPointerHandler.OnPointerDragged(
        MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), getIndexPosition(), Color.red);
            
            timer -= Time.deltaTime;
            if (timer <= 0){
                linePoints.Add(getIndexPosition());
                timer = timerdelay;
                drawLine.positionCount = linePoints.Count;
                drawLine.SetPositions(linePoints.ToArray());
            }
    }

  // Detecting the air tap gesture
    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            if (eventData.InputSource.SourceName == "Right Hand" || eventData.InputSource.SourceName == "Mixed Reality Controller Right")
            {
                    // Do something when the user does an air tap using their right hand only
                    Debug.Log(linePoints.Count);
                    newLine = new GameObject();
                    drawLine = newLine.AddComponent<LineRenderer>();
                    drawLine.material = new Material (Shader.Find("Sprites/Default"));
                    drawLine.startColor = Color.blue;
                    drawLine.endColor = Color.blue;
                    drawLine.startWidth = lineWidth;
                    drawLine.endWidth = lineWidth;
            }
        }

    public Vector3 getIndexPosition(){
        MixedRealityPose pose;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
            {
                return pose.Position;
            }
            return new Vector3(0,0,0);
    }
    public void OnInputDown(InputEventData eventData)
    {
        if (eventData.MixedRealityInputAction == grabAction)
        {
            Debug.Log(linePoints.Count);
            newLine = new GameObject();
            drawLine = newLine.AddComponent<LineRenderer>();
            drawLine.material = new Material (Shader.Find("Sprites/Default"));
            drawLine.startColor = Color.green;
            drawLine.endColor = Color.green;
            drawLine.startWidth = lineWidth;
            drawLine.endWidth = lineWidth;
        }
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (eventData.MixedRealityInputAction == grabAction)
        {
            linePoints.Clear();
        }
    }

    public void OnInputPressed(InputEventData<float> eventData) {
        if (eventData.MixedRealityInputAction == grabAction)
        {
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), getIndexPosition(), Color.red);
            
            timer -= Time.deltaTime;
            if (timer <= 0){
                linePoints.Add(getIndexPosition());
                timer = timerdelay;
                drawLine.positionCount = linePoints.Count;
                drawLine.SetPositions(linePoints.ToArray());
            }
        }
        }
    private void Awake()  {}
    public void OnPositionInputChanged(InputEventData<Vector2> eventData) { }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)  {}

    public void OnTouchStarted(HandTrackingInputEventData eventData) { }

    public void OnTouchUpdated(HandTrackingInputEventData eventData) {}

    void Start(){
        linePoints = new List<Vector3>();
        timer = timerdelay;
    }

    // void Update()
    // {
    //     Drawing();
    // }

    // void Drawing() 
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         newLine = new GameObject();
    //         drawLine = newLine.AddComponent<LineRenderer>();
    //         drawLine.material = new Material (Shader.Find("Sprites/Default"));
    //         drawLine.startColor = Color.red;
    //         drawLine.endColor = Color.red;
    //         drawLine.startWidth = lineWidth;
    //         drawLine.endWidth = lineWidth;
    //     } 

    //     if (Input.GetMouseButton(0))
    //     {
    //         Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), GetMousePosition(), Color.red);
            
    //         timer -= Time.deltaTime;
    //         if (timer <= 0){
    //             linePoints.Add(GetMousePosition());
    //             timer = timerdelay;
    //             drawLine.positionCount = linePoints.Count;
    //             drawLine.SetPositions(linePoints.ToArray());
    //         }
            
    //     }
        
    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         linePoints.Clear();
    //     }
    // }




    // Vector3 GetMousePosition(){
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     return ray.origin + ray.direction*10;
    // }

}
