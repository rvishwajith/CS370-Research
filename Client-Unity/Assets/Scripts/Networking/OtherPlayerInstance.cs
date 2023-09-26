using System;
using UnityEngine;

public class OtherPlayerInstance : MonoBehaviour
{
    [Header("Children")]
    [SerializeField] private Transform pivot;

    [Header("Identifier Data")]
    public long identifier = -1;
    public string username = "NoUsername";

    public RelayedPlayerDataPacket lastRelayedPacket;

    private void Start()
    {
        Debug.Log("OtherPlayerInstance: New player instance.");
        transform.name = "Other" + identifier;
    }

    private void Update()
    {
        transform.position = lastRelayedPacket.position;
        transform.rotation = lastRelayedPacket.bodyRotation;
        if (pivot != null)
            pivot.rotation = lastRelayedPacket.pivotRotation;
    }
}