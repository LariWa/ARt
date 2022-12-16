using UnityEngine;
using Unity.Networking.Transport;

public class Net_CreateMessage : NetMessage
{
  public int entryId { set; get; }
  public float red { set; get; }
  public float green { set; get; }
  public float blue { set; get; }
  public float width { set; get; }

  GameObject newLine;
  LineRenderer drawLine;

  public Net_CreateMessage()
  {
    Code = OpCode.CREATE_MSG;
  }

  public Net_CreateMessage(DataStreamReader reader)
  {
    Code = OpCode.CREATE_MSG;
    Deserialize(reader);
  }

  public Net_CreateMessage(int id, float r, float g, float b, float w)
  {
    Code = OpCode.CREATE_MSG;
    entryId = id;
    red = r;
    green = g;
    blue = b;
    width = w;
  }

  public override void Serialize(ref DataStreamWriter writer)
  {
    writer.WriteByte((byte)Code);
    writer.WriteInt(entryId);
    writer.WriteFloat(red);
    writer.WriteFloat(green);
    writer.WriteFloat(blue);
    writer.WriteFloat(width);
  }

  public override void Deserialize(DataStreamReader reader)
  {
    entryId = reader.ReadInt();
    red = reader.ReadFloat();
    green = reader.ReadFloat();
    blue = reader.ReadFloat();
    width = reader.ReadFloat();
  }

  public override void ReceivedOnServer()
  {
      Debug.Log("SERVER: New object created");
  }

  public override void ReceivedOnClient()
  {
      Debug.Log("CLIENT: New object created");

      newLine = new GameObject();
      newLine.transform.parent = BaseClient.instance.drawingOrigin;
      newLine.name = "Drawing " + entryId;
      newLine.tag = "drawing";

      drawLine = newLine.AddComponent<LineRenderer>();
      drawLine.positionCount--;

      Material material = Resources.Load("Materials/Paint", typeof(Material)) as Material;
      drawLine.material = material;
      //drawLine.material = new Material (Shader.Find("Sprites/Default"));
      drawLine.startColor = new Color(red, green, blue, 1);
      drawLine.endColor = new Color (red, green, blue, 1);

      drawLine.startWidth = width;
      drawLine.endWidth = width;
      drawLine.useWorldSpace = false;

    }

}
