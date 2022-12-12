using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using static LineBehavior;


using UnityEngine;

using UnityEngine;

//, IMixedRealityTouchHandler, IMixedRealityInputHandler
public class PaintBrush : MonoBehaviour, IMixedRealityPointerHandler
{
    List<Vector3> linePoints;
    float timer;
    bool drawing = false;
    System.DateTime previousDrawingTime;
    System.DateTime currentDrawingTime = System.DateTime.Now;
    public float timerdelay;
    public Material drawingMaterial;
    public Color lineColor = Color.red;

    GameObject newLine;
    LineRenderer drawLine;
    public float lineWidth;

    Vector2 lastPos;
        
    private MixedRealityInputAction grabAction = MixedRealityInputAction.None;

	

	void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
        if (drawing){
            linePoints.Clear();
            
            drawing = false;

            MeshCollider meshCollider = newLine.AddComponent<MeshCollider>();
            Mesh mesh = new Mesh();
            drawLine.BakeMesh(mesh);
            meshCollider.sharedMesh = mesh; //this the one that creates the second one!!!
            
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
            newLine.AddComponent<LineBehavior>();
            
            drawLine = newLine.AddComponent<LineRenderer>();
            drawLine.material =  drawingMaterial;  //new Material (Shader.Find("Sprites/Default"));
            drawLine.startColor = lineColor;
            drawLine.endColor = lineColor;
            drawLine.startWidth = lineWidth;
            drawLine.endWidth = lineWidth;
            
        }
    }

    void IMixedRealityPointerHandler.OnPointerDragged(
        MixedRealityPointerEventData eventData)
    {
        // Requirement for implementing the interface
        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), getIndexPosition(), lineColor);
            
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
        linePoints = new List<Vector3>();
        timer = timerdelay;
    }


}
   