using UnityEngine;

public struct ClientToServerPlayerPacket
{
    public long identifier;
    public Vector3 position;
    public Quaternion bodyRotation;
    public Quaternion pivotRotation;
}