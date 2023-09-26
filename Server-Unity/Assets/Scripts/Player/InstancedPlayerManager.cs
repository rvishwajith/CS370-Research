using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class InstancedPlayerManager : MonoBehaviour
{
    [Header("Prefabs")]
    public PlayerInstance playerInstancePrefab;

    private ConcurrentDictionary<long, PlayerInstance> idToPlayer = new();
    private ConcurrentQueue<ClientToServerPlayerPacket> pendingPlayers = new();

    public ICollection<long> Identifiers
    {
        get { return idToPlayer.Keys; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public bool TryGet(long id, out PlayerInstance instance)
    {
        return idToPlayer.TryGetValue(id, out instance);
    }

    /// <summary>
    /// Called once every frame. Calls HandlePendingRegistries() if necessary.
    /// </summary>
    private void Update()
    {
        HandlePendingRegistries();
    }

    /// <summary>
    /// Once each main thread update, pops values from the queue of pending identifiers and converts
    /// them into registered player instances.
    /// </summary>
    private void HandlePendingRegistries()
    {
        while (pendingPlayers.Count > 0)
        {
            ClientToServerPlayerPacket data;
            if (pendingPlayers.TryDequeue(out data))
            {
                RegisterPlayer(data);
                continue;
            }
            // Failed to register the player.
            Debug.Log("InstancedPlayerManager: ERROR - Failed to dequeue packet.");
            break;
        }
    }

    /// <summary>
    /// Registers a player data packet. If the identifier of the packet already exists, then the
    /// data will be sent to the corresponding controller. If not, a new panding registry will be
    /// added to the queue for use in the next frame update.
    /// </summary>
    /// <param name="packet">The data packet to register.</param>
    public void RegisterDataPacket(ClientToServerPlayerPacket packet)
    {
        PlayerInstance instance = null;
        if (idToPlayer.ContainsKey(packet.identifier))
        {
            if (idToPlayer.TryGetValue(packet.identifier, out instance))
                instance.latestPacket = packet;
        }
        else
        {
            pendingPlayers.Enqueue(packet);
            // Debug.Log("InstancedPlayerManager: Added pending registration packet.");
        }
    }

    /// <summary>
    /// Instantiate a player controller from a new identifier, and register the identifier in the
    /// data set. If the instance fails, the instance is destroyed.
    /// </summary>
    /// <param name="packet">The data packet to use for the player's initial instance daya</param>
    /// <returns>Whether the player was successfully registered.</returns>
    private bool RegisterPlayer(ClientToServerPlayerPacket packet)
    {
        if (playerInstancePrefab != null)
        {
            var identifier = packet.identifier;
            var instance = Instantiate(playerInstancePrefab, this.transform);
            if (idToPlayer.ContainsKey(identifier))
            {
                Destroy(instance.gameObject);
                return false;
            }
            if (idToPlayer.TryAdd(identifier, instance))
            {
                instance.name = "Player " + identifier;
                instance.latestPacket = packet;
                Debug.Log("InstancedPlayerManager: Player registered, ID: " + identifier);
                return true;
            }
            Debug.Log("InstancedPlayerManager: ERROR - Failed to register new player.");
            Destroy(instance.gameObject);
        }
        return false;
    }
}