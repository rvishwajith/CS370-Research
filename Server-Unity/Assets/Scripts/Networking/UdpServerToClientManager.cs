using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UdpServerToClientManager : MonoBehaviour
{
    [Header("Options")]
    [ReadOnly] private int relaySpeed = 64;
    [InfoBox("Packets/second sent to each UDP client.")]

    [Header("Managers")]
    [SerializeField] private InstancedPlayerManager playerManager;

    private Thread thread;
    private UdpSender sender;

    public void Start()
    {
        sender = new(destination: new(Addresses.Local, 6000));
        StartSender();
    }

    private void StartSender()
    {
        thread = new(() =>
        {
            var sleepTime = (int)(1000f / relaySpeed);
            try
            {
                Debug.Log("Started UDP relay, sleep time: " + sleepTime);
                while (true && playerManager != null)
                {
                    var relayBlock = MakeRelayBlock();
                    if (relayBlock.relayedData.Length > 0)
                    {
                        // Debug.Log(relayBlock.relayedData.Length);
                        sender.Send(JsonUtility.ToJson(relayBlock));
                    }
                    Thread.Sleep(sleepTime);
                }
            }
            catch (Exception) { Debug.Log("UDPServerToClient: ERROR - Unknown."); }
        })
        { IsBackground = true };
        thread.Start();
    }

    public ServerToClientRelayBlock MakeRelayBlock()
    {
        List<RelayedPlayerDataPacket> data = new();
        foreach (var id in playerManager.Identifiers)
        {
            PlayerInstance instance;
            if (!playerManager.TryGet(id, out instance))
            {
                Debug.Log("Get failed.");
                continue;
            }
            var latestPacket = instance.latestPacket;
            data.Add(new RelayedPlayerDataPacket()
            {
                identifier = id,
                username = "NoUsername",
                position = latestPacket.position,
                bodyRotation = latestPacket.bodyRotation,
                pivotRotation = latestPacket.pivotRotation,
            });
        }
        return new() { relayedData = data.ToArray() };
    }

    public void OnDestroy()
    {
        if (thread != null && thread.ThreadState == ThreadState.Running)
            thread.Abort();
    }
}