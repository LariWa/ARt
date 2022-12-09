using UnityEngine;
using Unity.Networking.Transport;
using System.Collections.Generic;
using Unity.Collections;

public class BaseServer : MonoBehaviour
{
    public NetworkDriver driver;
    protected NativeList<NetworkConnection> connections;
    public static BaseServer instance { get; private set; }

    private void Start()
    {
        instance = this;
        Init();
    }

    private void Update()
    {
        UpdateServer();
    }

    private void OnDestroy()
    {
        Shutdown();
    }

    public virtual void Init()
    {
        //initialize driver
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4; //anyone can connect
        endpoint.Port = 3355;

        if (driver.Bind(endpoint) != 0)
            Debug.Log("There was an error binding to port" + endpoint.Port);
        else driver.Listen();

        //initialize the connection list
        connections = new NativeList<NetworkConnection>(20, Allocator.Persistent); // maximum of players = 20
    }

    public virtual void UpdateServer()
    {
        driver.ScheduleUpdate().Complete();
        CleanUpConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }

    private void CleanUpConnections()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }

    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while ((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
            Debug.Log("Accepted a connection");
            //MazeGenerator.instance.sendToClient();
        }
    }

    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;
        for (int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = driver.PopEventForConnection(connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    OnData(stream);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    connections[i] = default(NetworkConnection);
                }
            }
        }
    }

    public virtual void OnData(DataStreamReader stream)
    {
        NetMessage msg = null;
        var opCode = (OpCode)stream.ReadInt();
        switch (opCode)
        {
            case OpCode.RENDERER_MSG: msg = new Net_RendererMessage(stream); break;
            case OpCode.CODE_MSG: msg = new Net_MsgCode(stream); break;

            default:
                Debug.Log("message received had no OpCode");
                break;
        }
        msg.ReceivedOnServer();

    }

    public virtual void SendToClient(NetMessage msg)
    {
        DataStreamWriter writer;
        if (connections.Length > 0)
        {
          for (int i = 0; i < connections.Length; i++)
          {
            driver.BeginSend(connections[i], out writer);
            msg.Serialize(ref writer);
            driver.EndSend(writer);
          }
        }
    }

    public virtual void Shutdown()
    {
        driver.Dispose();
        connections.Dispose();
    }
}
