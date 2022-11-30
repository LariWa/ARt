using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CalibratePoint : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("setAnchor", 5);
      

    }
    void setAnchor()
    {
        Debug.LogWarning("set anchor");
        var instance = Instantiate(prefab, this.transform.position, Quaternion.identity);
        var anchor = instance.AddComponent<ARAnchor>();
        PositionCalibrater.instance.addAnchor(anchor);
    }

}
