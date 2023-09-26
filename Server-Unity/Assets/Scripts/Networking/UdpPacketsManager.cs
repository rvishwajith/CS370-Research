using System.Collections.Concurrent;
using UnityEngine;

public class UdpPacketsManager : MonoBehaviour
{
    public ConcurrentDictionary<long, ClientToServerPlayerPacket> idToLatestPacket = new();
}

