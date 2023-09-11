using UnityEngine;

public struct PlayerDataPacket
{
    public long identifier;
    public Vector3 position;
    public Quaternion bodyRotation;
    public Quaternion pivotRotation;
}