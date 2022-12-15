using UnityEngine;
using Unity.Networking.Transport;

public class Net_EraseMessage : NetMessage
{

  public int entryId { set; get; }

  public Net_EraseMessage()
  {
    Code = OpCode.ERASE_MSG;
  }

  public Net_EraseMessage(DataStreamReader reader)
  {
    Code = OpCode.ERASE_MSG;
    Deserialize(reader);
  }

  public Net_EraseMessage(int id)
  {
    Code = OpCode.ERASE_MSG;
    entryId = id;
  }

  public override void Serialize(ref DataStreamWriter writer)
  {
    writer.WriteByte((byte)Code);
    writer.WriteInt(entryId);
  }

  public override void Deserialize(DataStreamReader reader)
  {
    entryId = reader.ReadInt();
  }

  public override void ReceivedOnServer()
  {
      Debug.Log("SERVER: erase drawing nr. " + entryId);
  }

  public override void ReceivedOnClient()
  {
      Debug.Log("CLIENT: erase drawing nr. " + entryId);

      GameObject drawing = GameObject.Find("Drawing " + entryId);
      GameObject.Destroy(drawing);
  }
}
