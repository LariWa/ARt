using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class PositionCalibrater : MonoBehaviour
{
    ARTrackedImageManager imgManager;
    public static PositionCalibrater instance;
    public List<ARAnchor> anchors;
    private void Awake()
    {
        instance = this;
        imgManager = FindObjectOfType<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        imgManager.trackedImagesChanged += OnImageChanged;

    }
    private void OnDisable()
    {
        imgManager.trackedImagesChanged -= OnImageChanged;
    }
    void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImg in args.added)
            Debug.Log(trackedImg.name);
    }
    public void addAnchor(ARAnchor anchor)
    {
        Debug.Log("add anchor to list");
        anchors.Add(anchor);
    }
}

