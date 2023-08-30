using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerData : MonoBehaviour
{
    public bool showUsernameAndID = true;
    public string username = "Username";
    public string sessionToken = "TokenID";

    private void OnDrawGizmos()
    {
        void AddUsernameAndIDLabels()
        {
            var pos = transform.position + Vector3.up * 2.5f;
            var text = "User: " + username + "\nToken: " + sessionToken;
            Handles.Label(pos, text, DebugProperties.BlackOnYellowStyle);
        }

        if (showUsernameAndID)
        {
            AddUsernameAndIDLabels();
        }
    }
}