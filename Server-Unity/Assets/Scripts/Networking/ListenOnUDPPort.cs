using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using NaughtyAttributes;

public class ListenOnUDPPort : MonoBehaviour
{
    public PlayerDataManager manager;

    private UdpClient listener;
    private Thread thread;

    private void Start()
    {
        void CreateListener()
        {
            listener = new();
            thread = new(() =>
            {
                var senderEP = new IPEndPoint(Addresses.Local, 5000);
                // Debug.Log("Started listening.");
                listener.Client.Bind(senderEP);
                try
                {
                    while (true)
                    {
                        var bytes = listener.Receive(ref senderEP);
                        var str = Encoding.ASCII.GetString(bytes);
                        // Debug.Log("Recieved packet of player data.");
                        if (manager != null)
                        {
                            var packet = JsonUtility.FromJson<PlayerDataPacket>(str);
                            manager.RegisterDataPacket(packet);
                        }
                        // Minimum delay of 5 ms between each packet.
                        Thread.Sleep(5);
                    }
                }
                catch (ThreadAbortException)
                {
                    Debug.Log("Closed thread.");
                    thread.Abort();
                }
                catch (SocketException)
                {
                    Debug.Log("ListenOnUDPPort.ListenerThread: Closing thread (SocketException).");
                }
                catch (ObjectDisposedException)
                {
                    Debug.Log("ListenOnUDPPort.ListenerThread: ERROR. Socket was already closed");
                }
            });
            thread.IsBackground = true;
        }
        CreateListener();
        thread.Start();
    }

    private void Kill()
    {
        if (thread != null && thread.ThreadState == ThreadState.Running)
            thread.Abort();
    }

    private void OnDestroy()
    {
        Kill();
    }
}