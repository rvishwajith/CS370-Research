using System.Collections.Concurrent;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [Header("Prefabs")]
    public PlayerInstance playerInstancePrefab;

    private ConcurrentDictionary<long, PlayerInstance> registrations = new();
    private ConcurrentQueue<PlayerDataPacket> pendingRegistries = new();

    /// <summary>
    /// Called once every frame. Calls HandlePendingRegistries() if necessary.
    /// </summary>
    private void Update()
    {
        HandlePendingRegistries();
    }

    /// <summary>
    /// Once every (render) frame, pops values from the queue of pending identifiers and converts
    /// them into registered player instances.
    /// </summary>
    private void HandlePendingRegistries()
    {
        while (pendingRegistries.Count != 0)
        {
            PlayerDataPacket data;
            if (pendingRegistries.TryDequeue(out data))
            {
                RegisterPlayer(data);
            }
            else
            {
                Debug.Log("PlayerDataManager.Update() ERROR: Failed to dequeue packet.");
                return;
            }
        }
    }

    /// <summary>
    /// Registers a player data packet. If the identifier of the packet already exists, then the
    /// data will be sent to the corresponding controller. If not, a new panding registry will be
    /// added to the queue for use in the next frame update.
    /// </summary>
    /// <param name="packet">The data packet to register.</param>
    public void RegisterDataPacket(PlayerDataPacket packet)
    {
        PlayerInstance instance = null;
        if (registrations.TryGetValue(packet.identifier, out instance))
        {
            instance.latestPacket = packet;
        }
        else
        {
            pendingRegistries.Enqueue(packet);
            // Debug.Log("PlayerDataManager.RegisterData(): Added pending registration packet.");
        }
    }

    /// <summary>
    /// Instantiate a player controller from a new identifier, and register the identifier in the
    /// data set.
    /// </summary>
    /// <param name="packet">The data packet to use for the player's initial instance daya</param>
    /// <returns></returns>
    private bool RegisterPlayer(PlayerDataPacket packet)
    {
        if (playerInstancePrefab != null)
        {
            var identifier = packet.identifier;
            var instance = Instantiate(playerInstancePrefab, this.transform);
            if (registrations.ContainsKey(identifier))
            {
                Destroy(instance.gameObject);
                return false;
            }
            if (registrations.TryAdd(identifier, instance))
            {
                instance.name = "Player " + identifier;
                instance.latestPacket = packet;
                Debug.Log("PlayerDataManager.RegisterPlayer(): Player registered, ID: " + identifier);
                return true;
            }
            else
            {
                Debug.Log("PlayerDataManager.RegisterPlayer() ERROR: Failed to register new player.");
                Destroy(instance.gameObject);
            }
        }
        return false;
    }
}