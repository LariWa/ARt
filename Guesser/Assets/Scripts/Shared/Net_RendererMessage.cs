using UnityEngine;
using Unity.Networking.Transport;

public class Net_RendererMessage : NetMessage
{
  public int entryId { set; get; }
  public float posX { set; get; }
  public float posY { set; get; }
  public float posZ { set; get; }
  //public Color color { set; get; }
  //public int brush { set; get; }

  public Net_RendererMessage()
  {
    Code = OpCode.RENDERER_MSG;
  }

  public Net_RendererMessage(DataStreamReader reader)
  {
    Code = OpCode.RENDERER_MSG;
    Deserialize(reader);
  }

  public Net_RendererMessage(float x, float y, float z, int id)
  {
    Code = OpCode.RENDERER_MSG;
    entryId = id;
    posX = x;
    posY = y;
    posZ = z;
    //color = c;
    //brush = b;
  }

  public override void Serialize(ref DataStreamWriter writer)
  {
    writer.WriteByte((byte)Code);
    writer.WriteInt(entryId);
    writer.WriteFloat(posX);
    writer.WriteFloat(posY);
    writer.WriteFloat(posZ);
    //writer.WriteColor(color);
    //writer.WriteInt(brush);
  }

  public override void Deserialize(DataStreamReader reader)
  {
    entryId = reader.ReadInt();
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
      Debug.Log("CLIENT:" + posX + "/" + posY + "/" + posZ);
  }

}
