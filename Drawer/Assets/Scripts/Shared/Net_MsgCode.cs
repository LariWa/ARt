using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class Net_MsgCode : NetMessage
{
    // 0-8 OP CODE
    public actionTypeCode actionType;

    public Net_MsgCode(actionTypeCode actionType)
    {
        Code = OpCode.CODE_MSG;
        this.actionType = actionType;
    }
    public Net_MsgCode(DataStreamReader reader)
    {
        Code = OpCode.CODE_MSG;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteByte((byte)actionType);
    }
    public override void Deserialize(DataStreamReader reader)
    {
        actionType = (actionTypeCode)reader.ReadByte();
    }

    public override void ReceivedOnServer()
    {
        if (actionType == actionTypeCode.RESET)
        {

            Debug.Log(("restart"));
            GameObject theTimer = GameObject.Find("Timer");
            TimerController timerScript = theTimer.GetComponent<TimerController>(); //get the timer script from the object
            timerScript.setTimeRemaining(120.0f);
            timerScript.setIsRunning(true);

            GameObject theScore = GameObject.Find("Score");
            ScoreController scoreScript = theScore.GetComponent<ScoreController>();
            scoreScript.resetScore();

            //erase drawings
            var drawings = GameObject.FindGameObjectsWithTag("drawing");
            foreach (GameObject drawing in drawings)
            {
                GameObject.Destroy(drawing);
            }
            ////reposition objects
            //eraser.transform.position = eraserPosition;
            //highlighter.transform.position = highlighterPosition;
            //brush.transform.position = brushPosition;
            //pencil.transform.position = pencilPosition;
            //palette.transform.position = palettePosition;
        }
    }
    public override void ReceivedOnClient()
    {
    }
}
