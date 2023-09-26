using UnityEngine;

public class LockFrameRate : MonoBehaviour
{
    public int targetFrameRate = 60;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
