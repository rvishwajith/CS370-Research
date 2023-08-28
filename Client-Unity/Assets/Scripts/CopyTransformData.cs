using UnityEngine;

public class CopyTransformData : MonoBehaviour
{
    public Transform target;

    [Header("Copying Settings")]
    public bool copyPosition = true;
    public bool copyRotation = true;

    private void LateUpdate()
    {
        if (target == null)
            return;
        if (copyPosition)
            transform.position = target.position;
        if (copyRotation)
            transform.rotation = target.rotation;
    }
}
