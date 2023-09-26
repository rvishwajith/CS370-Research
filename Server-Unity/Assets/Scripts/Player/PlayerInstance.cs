using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

public class PlayerInstance : MonoBehaviour
{
    [Header("Heirarchy")]
    [SerializeField] private Transform directionPivot;

    [Header("Player Data")]
    [ReadOnly] public string username;

    [Header("Debug Options")]
    public bool drawLineOfSight = true;
    public bool showUsernameAndID = true;

    /// <summary>
    /// The latest data packet associated with this player's identifier that has been recieved from
    /// the client.
    /// </summary>
    public ClientToServerPlayerPacket latestPacket;

    /// <summary>
    /// Called on instantiation of the object. Sets default values for the global variables as
    /// necessary.
    /// </summary>
    private void Start()
    {
        username = "NoUsername";
        if (directionPivot == null && transform.childCount > 0)
        {
            directionPivot = transform.GetChild(0);
        }
    }

    /// <summary>
    /// Called once every frame. Updates the player's transformation data from the latest player
    /// data packet.
    /// </summary>
    private void Update()
    {
        transform.position = latestPacket.position;
        transform.rotation = latestPacket.bodyRotation;
        if (directionPivot != null)
        {
            directionPivot.rotation = latestPacket.pivotRotation;
        }
    }

    private void OnDrawGizmos()
    {
        if (showUsernameAndID)
            AddUsernameAndIDLabels();
        if (drawLineOfSight)
            DrawLineOfSight();

        void DrawLineOfSight()
        {
            if (directionPivot == null)
                return;
            var origin = directionPivot.position;
            var direction = directionPivot.forward;
            var length = 100;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(origin, origin + direction * length);
        }

        void AddUsernameAndIDLabels()
        {
            var pos = transform.position + Vector3.up * 2.5f;
            var text = "ID: " + latestPacket.identifier;
            Handles.Label(pos, text, DebugProperties.BlackOnYellowStyle);
        }
    }
}