
using UnityEngine;
using Unity.Networking.Transport;
using System.Collections.Generic;
using Unity.Collections;
using System;

public class BaseClient : MonoBehaviour
{
    public NetworkDriver driver;
    protected NetworkConnection connection;
    public bool isConnected;
    public static BaseClient instance { get; private set; }
    public float connectInterval = 1f;
    private float lastTry;
    string ipServer;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if(connection.IsCreated)
        UpdateClient();
        else if(timePassed())
        {
            lastTry = Time.time;
            Init(ipServer);
        }
    }

    bool timePassed()
    {
        return Time.time - lastTry > connectInterval;
    }

    private void OnDestroy()
    {
        Shutdown();
    }

    public virtual void Init(string ip)
    {
        //initialize driver
        ipServer = ip;
        driver = NetworkDriver.Create();
        connection = default(NetworkConnection);

        NetworkEndPoint endpoint;
        NetworkEndPoint.TryParse(ip, 3355, out endpoint);
        connection = driver.Connect(endpoint);
    }

    public virtual void UpdateClient()
    {
        driver.ScheduleUpdate().Complete();
        CheckAlive();
        UpdateMessagePump();
    }

    private void CheckAlive()
    {
        if (!connection.IsCreated)
        {
            Debug.Log("Lost connection to server");
            isConnected = false;
        }
    }

    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;
        NetworkEvent.Type cmd;

        while ((cmd = driver.PopEventForConnection(connection, out stream)) != NetworkEvent.Type.Empty)
        //while ((cmd = connection.PopEventForConnection(driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Data)
            {
                OnData(stream);
            }
            else if (cmd == NetworkEvent.Type.Connect)
            {
                isConnected = true;
                Debug.Log("connected to server");
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client got disconnected from server");
                connection = default(NetworkConnection);
                isConnected = false;
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
        msg.ReceivedOnClient();
    }

    public virtual void Shutdown()
    {
        driver.Dispose();
    }

    public virtual void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }
}
