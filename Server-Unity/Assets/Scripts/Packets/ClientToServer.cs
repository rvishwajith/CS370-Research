using UnityEngine;

/// <summary>
/// A packet that is stored data sent from the client to the server (UDP) about an individual
/// player's data (position, rotation, etc.)
/// </summary>
public struct ClientToServerPlayerPacket
{
    public long identifier;
    public Vector3 position;
    public Quaternion bodyRotation;
    public Quaternion pivotRotation;
}