using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Net;
using System.Net.Sockets;

public class UdpClientToServerManager : MonoBehaviour
{
    public Transform player;
    private UdpSender udpSender;

    private void Start()
    {
        Identifiers.RegeneratePlayerID();
        udpSender = new(destination: new(Addresses.Local, 5000));
        DOVirtual.DelayedCall(1f / 64, () =>
        {
            if (player != null)
                udpSender.Send(GetPlayerData());
        }).SetLoops(-1);
    }

    private string GetPlayerData()
    {
        var packet = new ClientToServerPlayerPacket();
        packet.identifier = Identifiers.PlayerID;
        packet.position = player.position;
        packet.bodyRotation = player.rotation;
        packet.pivotRotation = player.GetChild(0).rotation;
        return JsonUtility.ToJson(packet);
    }

    private void OnDestroy()
    {
        if (udpSender != null)
            udpSender.Kill();
    }
}