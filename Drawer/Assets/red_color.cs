using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_collider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("collided ! ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
