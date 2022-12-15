using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redCollider : MonoBehaviour
{
    ColorManager colorManager;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        colorManager = FindObjectOfType<ColorManager>();
    }

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("collided ! ");
        
    }

    void OnTriggerEnter(Collider collision) 
    {
        Debug.Log("Went in! ");
        
    }

    void OnTriggerExit(Collider collision) 
    {
        colorManager.onChangeColor(color);
        Debug.Log(Color.red);
        Debug.Log(color);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}