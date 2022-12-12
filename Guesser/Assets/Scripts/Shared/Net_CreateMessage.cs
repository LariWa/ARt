using UnityEngine;
using Unity.Networking.Transport;
//using static LineBehavior;

public class Net_CreateMessage : NetMessage
{
  public int entryId { set; get; }
  public int color { set; get; }
  public int brush { set; get; }
  // material

  GameObject newLine;
  LineRenderer drawLine;
  float lineWidth;

  public Net_CreateMessage()
  {
    Code = OpCode.CREATE_MSG;
  }

  public Net_CreateMessage(DataStreamReader reader)
  {
    Code = OpCode.CREATE_MSG;
    Deserialize(reader);
  }

  public Net_CreateMessage(int id, int c, int b)
  {
    Code = OpCode.CREATE_MSG;
    entryId = id;
    color = c;
    brush = b;
  }

  public override void Serialize(ref DataStreamWriter writer)
  {
    writer.WriteByte((byte)Code);
    writer.WriteInt(entryId);
    writer.WriteInt(color);
    writer.WriteInt(brush);
  }

  public override void Deserialize(DataStreamReader reader)
  {
    entryId = reader.ReadInt();
    color = reader.ReadInt();
    brush = reader.ReadInt();
  }

  public override void ReceivedOnServer()
  {
      Debug.Log("SERVER: New object created");
  }

  public override void ReceivedOnClient()
  {
      Debug.Log("CLIENT: New object created");

      newLine = new GameObject();
      newLine.name = "Drawing " + entryId;
      //newLine.AddComponent<LineBehavior>();

      drawLine = newLine.AddComponent<LineRenderer>();
      drawLine.material = new Material (Shader.Find("Sprites/Default"));
      drawLine.startColor = Color.red; // replace with color
      drawLine.endColor = Color.red;
      drawLine.startWidth = lineWidth; // replace with brush
      drawLine.endWidth = lineWidth;

  }

}
