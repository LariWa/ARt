using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using static LineBehavior;

using UnityEngine;

//, IMixedRealityTouchHandler, IMixedRealityInputHandler
public class Brush : MonoBehaviour, IMixedRealityPointerHandler
{
    List<Vector3> linePoints;
    float timer;
    bool drawing = false;
    System.DateTime previousDrawingTime;
    System.DateTime currentDrawingTime = System.DateTime.Now;
    public float timerdelay;

    GameObject newLine;
    LineRenderer drawLine;
    public float lineWidth;

    Vector2 lastPos;

    private BaseServer server;

    private MixedRealityInputAction grabAction = MixedRealityInputAction.None;

	void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
        if (drawing){
            linePoints.Clear();
            drawing = false;
        }

    }

    void IMixedRealityPointerHandler.OnPointerDown(
       MixedRealityPointerEventData eventData)
    {
        previousDrawingTime = currentDrawingTime;
        currentDrawingTime = System.DateTime.Now;
        System.TimeSpan diff = currentDrawingTime - previousDrawingTime;
		double seconds = diff.TotalSeconds;
        if (seconds <= 2){
            drawing = true;
        }

        // Requirement for implementing the interface
        if (drawing){
            Debug.Log(linePoints.Count);
            newLine = new GameObject();
            newLine.name = "Drawing";
            newLine.AddComponent<LineBehavior>();
            // newLine.AddComponent(IMixedRealityInputHandler);
            drawLine = newLine.AddComponent<LineRenderer>();
            drawLine.material = new Material (Shader.Find("Sprites/Default"));
            drawLine.startColor = Color.red;
            drawLine.endColor = Color.red;
            drawLine.startWidth = lineWidth;
            drawLine.endWidth = lineWidth;

        }
    }

    void IMixedRealityPointerHandler.OnPointerDragged(
        MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), getIndexPosition(), Color.red);

            if (drawing){
                timer -= Time.deltaTime;
                if (timer <= 0){
                    linePoints.Add(getIndexPosition());
                    timer = timerdelay;
                    drawLine.positionCount = linePoints.Count;
                    drawLine.SetPositions(linePoints.ToArray());
                }
            }
    }

  // Detecting the air tap gesture
    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            // if (eventData.InputSource.SourceName == "Right Hand" || eventData.InputSource.SourceName == "Mixed Reality Controller Right")
            // {
            //         // Do something when the user does an air tap using their right hand only
            //         Debug.Log(linePoints.Count);
            //         newLine = new GameObject();
            //         drawLine = newLine.AddComponent<LineRenderer>();
            //         drawLine.material = new Material (Shader.Find("Sprites/Default"));
            //         drawLine.startColor = Color.blue;
            //         drawLine.endColor = Color.blue;
            //         drawLine.startWidth = lineWidth;
            //         drawLine.endWidth = lineWidth;
            // }


        }

    public Vector3 getIndexPosition(){
        MixedRealityPose pose;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
            {
                return pose.Position;
            }
            return new Vector3(0,0,0);
    }

    private void Awake()  {}


    void Start(){
        server = FindObjectOfType<BaseServer>();
        linePoints = new List<Vector3>();
        timer = timerdelay;
    }


}
