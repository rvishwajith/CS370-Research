using System.Collections.Concurrent;
using UnityEngine;

public class OtherPlayerInstanceManager : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private OtherPlayerInstance otherPlayerPrefab;

    private ConcurrentDictionary<long, OtherPlayerInstance> iDsToInstances = new();
    private ConcurrentQueue<RelayedPlayerDataPacket> pendingInstances = new();

    public void Update()
    {
        if (otherPlayerPrefab == null)
            return;
        while (!pendingInstances.IsEmpty)
        {
            RelayedPlayerDataPacket otherPlayerPacket;
            if (pendingInstances.TryDequeue(out otherPlayerPacket))
            {
                var instance = AddInstance(otherPlayerPacket);
                if (TryRegisterInstance(instance))
                {
                    Debug.Log("Added other player instance.");
                    continue;
                }
                Debug.Log("Couldn't add other player instance to dictionary, destroying.");
                break;
            }
            Debug.Log("OtherPlayerInstanceManager: Failed to dequeue new other player packet.");
            break;
        }
    }

    private OtherPlayerInstance AddInstance(RelayedPlayerDataPacket packet)
    {
        var otherPlayer = Instantiate(otherPlayerPrefab);
        otherPlayer.lastRelayedPacket = packet;
        otherPlayer.identifier = packet.identifier;
        otherPlayer.username = packet.username;
        return otherPlayer;
    }

    private bool TryRegisterInstance(OtherPlayerInstance instance)
    {
        if (!iDsToInstances.ContainsKey(instance.identifier))
        {
            if (iDsToInstances.TryAdd(instance.identifier, instance))
                return true;
        }
        Destroy(instance, 0.1f);
        return false;
    }

    public void ProcessRelayBlock(ServerToClientRelayBlock block)
    {
        if (block.relayedData == null)
        {
            // Debug.Log("Processed block is null.");
            return;
        }
        foreach (var packet in block.relayedData)
        {
            var id = packet.identifier;
            if (id == Identifiers.PlayerID)
                continue;
            // Debug.Log(id);
            if (iDsToInstances.ContainsKey(id))
            {
                OtherPlayerInstance otherPlayer;
                if (iDsToInstances.TryGetValue(id, out otherPlayer))
                {
                    otherPlayer.lastRelayedPacket = packet;
                    continue;
                }
                Debug.Log("OtherPlayerInstanceManager: ERROR - Failed to get already existing key.");
            }
            // Debug.Log("OtherPlayerInstanceManager: Registering a new player.");
            pendingInstances.Enqueue(packet);
        }
    }
}