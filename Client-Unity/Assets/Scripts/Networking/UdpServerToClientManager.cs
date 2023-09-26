using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Text;

public class UdpServerToClientManager : MonoBehaviour
{
    private UdpClient listener;
    private Thread thread;

    public OtherPlayerInstanceManager otherPlayerManager;

    public void Start()
    {
        listener = new();
        StartListening();
    }

    private void StartListening()
    {
        thread = new(() =>
        {
            var listenerLocation = new IPEndPoint(Addresses.Local, 6000);
            Debug.Log("Started listening for UDP server packets.");
            try
            {
                listener.Client.Bind(listenerLocation);
                while (true && otherPlayerManager != null)
                {
                    var bytes = listener.Receive(ref listenerLocation);
                    var str = Encoding.ASCII.GetString(bytes);
                    var block = JsonUtility.FromJson<ServerToClientRelayBlock>(str);
                    otherPlayerManager.ProcessRelayBlock(block);
                    Thread.Sleep(10);
                }
            }
            catch (ThreadAbortException)
            {
                Debug.Log("UdpClientToServer: Closed relay listener thread.");
                thread.Abort();
            }
        })
        { IsBackground = true };
        thread.Start();
    }

    private void OnDestroy()
    {
        if (thread != null && thread.ThreadState == ThreadState.Running)
            thread.Abort();
    }
}

