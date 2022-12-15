using UnityEngine;
using Unity.Networking.Transport;

public class Net_CreateMessage : NetMessage
{
  public int entryId { set; get; }
  public int color { set; get; }
  public int brush { set; get; }

  GameObject newLine;
  LineRenderer drawLine;
  float lineWidth = 0.01f;
  static Color orange =  new Color(1, 0.5f, 0, 1);
  Color[] colors = new Color[] { Color.red, orange, Color.yellow, Color.green, Color.blue, Color.magenta, Color.white, Color.black };
  string[] materials = { "Paint", "Highlight", "Paint" };

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
      newLine.transform.parent = BaseClient.instance.drawingOrigin;
      newLine.name = "Drawing " + entryId;
      newLine.tag = "drawing";

      drawLine = newLine.AddComponent<LineRenderer>();
      drawLine.positionCount--;

      //Material material = (Material)Resources.Load(materials[brush], typeof(Material));
      //drawLine.material = material;
      drawLine.material = new Material (Shader.Find("Sprites/Default"));
      drawLine.startColor = colors[color];
      drawLine.endColor = colors[color];
      drawLine.startWidth = lineWidth; // replace with brush
      drawLine.endWidth = lineWidth;
      drawLine.useWorldSpace = false;

    }

}
