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
    public Color lineColor = Color.green;
    public Material drawingMaterial;

    GameObject newLine;
    LineRenderer drawLine;
    public float lineWidth;
    GameObject drawingOrigin;
    Vector2 lastPos;

    private MixedRealityInputAction grabAction = MixedRealityInputAction.None;

    private BaseServer server;
    private int id = -1;


   
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

            id++;
            newLine = new GameObject();
            newLine.transform.parent = drawingOrigin.transform;
            newLine.name = "Drawing " + id;
            newLine.AddComponent<LineBehavior>();

            // let client know that a new drawing has been created
            Net_CreateMessage msg;
            msg = new Net_CreateMessage(id, 0, 0);
            server.SendToClient(msg);
            Debug.Log(newLine.transform.position);
            drawLine = newLine.AddComponent<LineRenderer>();
            drawLine.material =  drawingMaterial;  //new Material (Shader.Find("Sprites/Default"));
            drawLine.startColor = lineColor;
            drawLine.endColor = lineColor;
            drawLine.startWidth = lineWidth;
            drawLine.endWidth = lineWidth;
            drawLine.useWorldSpace = false;
            Debug.Log(newLine.transform.position);

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


                    // update linePoints on client
                    Net_RendererMessage msg;
                    Vector3 points = drawingOrigin.transform.InverseTransformPoint(getIndexPosition());
                    msg = new Net_RendererMessage(id, linePoints.Count, points.x, points.y, points.z);
                    server.SendToClient(msg);
                }
            }
    }

  // Detecting the air tap gesture
    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
        {

        }

    public Vector3 getIndexPosition(){
        /*MixedRealityPose pose;
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
            {
                return pose.Position;
            }
            return new Vector3(0,0,0);*/
        Transform child = transform.Find("Tip");
        return child.position;
    }

    private void Awake()  {}


    void Start(){
        linePoints = new List<Vector3>();
        timer = timerdelay;
        drawingOrigin = GameObject.Find("drawingOrigin");
        server = FindObjectOfType<BaseServer>();
    }


}
