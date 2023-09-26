using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class UdpClientToServerManager : MonoBehaviour
{
    public InstancedPlayerManager playerManager;

    private UdpClient listener;
    private Thread thread;

    private void Start()
    {
        listener = new();
        StartListening();
    }

    private void StartListening()
    {
        thread = new(() =>
        {
            var listenerLocation = new IPEndPoint(Addresses.Local, 5000);
            Debug.Log("Listening for UDP client packets.");
            try
            {
                listener.Client.Bind(listenerLocation);
                while (true)
                {
                    var bytes = listener.Receive(ref listenerLocation);
                    var str = Encoding.ASCII.GetString(bytes);
                    // Debug.Log("Recieved packet of player data.");
                    if (playerManager == null)
                        break;
                    var packet = JsonUtility.FromJson<ClientToServerPlayerPacket>(str);
                    playerManager.RegisterDataPacket(packet);
                    // Minimum delay of 5 ms between each packet.
                    Thread.Sleep(5);
                }
            }
            catch (ThreadAbortException)
            {
                Debug.Log("UdpClientToServer: Closed thread.");
                thread.Abort();
            }
            catch (SocketException)
            {
                // Debug.Log("UdpClientToServer: Closing listener socket (SocketException).");
                listener.Close();
            }
            catch (ObjectDisposedException)
            {
                Debug.Log("UdpClientToServer: ListenerThread socket was already closed.");
            }
            listener.Close();
        })
        { IsBackground = true };
        thread.Start();
    }

    private void OnDestroy()
    {
        if (listener != null)
            listener.Close();
        if (thread != null && thread.ThreadState == ThreadState.Running)
            thread.Abort();
    }
}