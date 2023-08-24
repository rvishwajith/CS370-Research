using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] private Transform xPivotTransform;
    [SerializeField] private PlayerInputSettings inputSettings;

    private float xPivotTurn = 0, yPivotTurn = 0;

    private void Start()
    {
        transform.eulerAngles = Vector3.Scale(transform.eulerAngles, Vector3.up);
    }

    private void Update()
    {
        if (Application.isFocused)
        {
            Rotation();
        }
    }

    private void Rotation()
    {
        var mouseInput = CustomInput.MouseInput(clampMagnitude: false, useDeltaTime: true);
        var worldUp = Vector3.up;

        void YAxisRotation()
        {
            yPivotTurn += mouseInput.y * inputSettings.sens;
            var forward = Vector3.SlerpUnclamped(Vector3.forward, Vector3.right, yPivotTurn);
            transform.rotation = Quaternion.LookRotation(forward, worldUp);
        }

        void XAxisRotation()
        {
            xPivotTurn += mouseInput.x * inputSettings.sens;
            xPivotTurn = Mathf.Clamp(xPivotTurn, inputSettings.xTurnRange.x, inputSettings.xTurnRange.y);
            var forward = Vector3.SlerpUnclamped(Vector3.forward, Vector3.up, xPivotTurn);
            xPivotTransform.localRotation = Quaternion.LookRotation(forward, worldUp);
        }

        YAxisRotation();
        if (xPivotTransform != null)
        {
            XAxisRotation();
        }
    }

    private void FixedUpdate()
    {
        if (Application.isFocused)
        {
            MoveHorizontal();
        }
    }

    private void MoveHorizontal()
    {
        if (rb == null)
            return;

        var dir = transform.TransformDirection(CustomInput.MultiAxis(axis: "Move XZ", adjust: true));
        var force = dir * inputSettings.pushForce;
        rb.AddForce(force, ForceMode.Acceleration);
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void OnDrawGizmos()
    {
        if (xPivotTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(xPivotTransform.position, xPivotTransform.forward * 100f);
        }
    }
}

[Serializable]
public class PlayerInputSettings
{
    [SerializeField] private PlayerInputAsset inputAsset;

    public float playerSens = 1;
    public float sens
    {
        get
        {
            return playerSens * inputAsset.sensMultiplier;
        }
    }

    public float pushForce
    {
        get
        {
            return inputAsset.pushForceMultiplier;
        }
    }

    public Vector2 xTurnRange = new(-0.8f, 0.9f);
}