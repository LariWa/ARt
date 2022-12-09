using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TMP_InputField inputIP;

    void Update()
    {
        if (BaseClient.instance.isConnected)
        {
            this.gameObject.SetActive(false);
        }
    }
    
    public void OnClickConnect()
    {
        BaseClient.instance.Init(inputIP.text);
    }
}
