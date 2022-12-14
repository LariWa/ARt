using UnityEngine;
using Unity.Networking.Transport;
//using static LineBehavior;

public class Net_RendererMessage : NetMessage
{
  public int entryId { set; get; }
  public int count { set; get; }
  public float posX { set; get; }
  public float posY { set; get; }
  public float posZ { set; get; }

  public Net_RendererMessage()
  {
    Code = OpCode.RENDERER_MSG;
  }

  public Net_RendererMessage(DataStreamReader reader)
  {
    Code = OpCode.RENDERER_MSG;
    Deserialize(reader);
  }

  public Net_RendererMessage(int id, int c, float x, float y, float z)
  {
    Code = OpCode.RENDERER_MSG;
    entryId = id;
    count = c;
    posX = x;
    posY = y;
    posZ = z;
  }

  public override void Serialize(ref DataStreamWriter writer)
  {
    writer.WriteByte((byte)Code);
    writer.WriteInt(entryId);
    writer.WriteInt(count);
    writer.WriteFloat(posX);
    writer.WriteFloat(posY);
    writer.WriteFloat(posZ);
  }

  public override void Deserialize(DataStreamReader reader)
  {
    entryId = reader.ReadInt();
    count = reader.ReadInt();
    posX = reader.ReadFloat();
    posY = reader.ReadFloat();
    posZ = reader.ReadFloat();
  }

  public override void ReceivedOnServer()
  {
      Debug.Log("SERVER:" + posX + "/" + posY + "/" + posZ);
  }

  public override void ReceivedOnClient()
  {
      Debug.Log("Entry " + count + ": " + posX + "/" + posY + "/" + posZ);


      GameObject line = GameObject.Find("Drawing " + entryId);
      if (line != null) {
        Debug.Log("Found");
      }

      LineRenderer drawLine = line.GetComponent(typeof(LineRenderer)) as LineRenderer;
      drawLine.positionCount++;
      drawLine.SetPosition(count - 1, BaseClient.instance.drawingOrigin.TransformPoint( new Vector3(posX, posY, posZ)));
  }

}
