using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void restartGame()
    {
        BaseClient.instance.SendToServer(new Net_MsgCode(actionTypeCode.RESET));
        var drawings = GameObject.FindGameObjectsWithTag("drawing");
        foreach (GameObject drawing in drawings)
        {
            GameObject.Destroy(drawing);
        }
    }
    public void correctGuess()
    {
        BaseClient.instance.SendToServer(new Net_MsgCode(actionTypeCode.CORRECTREPLY));
    }
}
