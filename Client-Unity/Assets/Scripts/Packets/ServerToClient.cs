using UnityEngine;
using System;

public struct ServerToClientRelayBlock
{
    [SerializeField] public RelayedPlayerDataPacket[] relayedData;
}

[Serializable]
public struct RelayedPlayerDataPacket
{
    public long identifier;
    public string username;
    public Vector3 position;
    public Quaternion bodyRotation;
    public Quaternion pivotRotation;
}