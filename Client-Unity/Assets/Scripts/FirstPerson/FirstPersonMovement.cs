using System;
using UnityEngine;
using NaughtyAttributes;

public class FirstPersonMovement : MonoBehaviour
{
    new public Rigidbody rigidbody;

    [SerializeField] private Transform xPivotTransform;
    [SerializeField] private PlayerInputSettings inputSettings;
    [SerializeField] private Transform groundedRayOrigin;

    // [ReadOnly] private bool isGrounded = false;
    private float xPivotTurn = 0, yPivotTurn = 0;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
        xPivotTransform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Application.isFocused)
        {
            Rotate();
            if (IsGrounded()) Jump();
        }
    }

    private bool IsGrounded()
    {
        if (groundedRayOrigin == null) return false;

        var origin = groundedRayOrigin.position;
        var dir = -Vector3.up;
        var tolerance = groundedRayOrigin.localPosition.y + 0.05f;
        return Physics.Raycast(new Ray(origin, dir), tolerance);
    }

    private void Rotate()
    {
        var mouseInput = CustomInput.MouseInput(useDeltaTime: true);
        var worldUp = Vector3.up;

        void RotateY()
        {
            yPivotTurn += mouseInput.y * inputSettings.sens;
            var forward = Vector3.SlerpUnclamped(Vector3.forward, Vector3.right, yPivotTurn);
            transform.rotation = Quaternion.LookRotation(forward, worldUp);
        }

        void RotatePivotX()
        {
            xPivotTurn += mouseInput.x * inputSettings.sens;
            xPivotTurn = Mathf.Clamp(xPivotTurn, inputSettings.xTurnRange.x, inputSettings.xTurnRange.y);
            var forward = Vector3.SlerpUnclamped(Vector3.forward, Vector3.up, xPivotTurn);
            xPivotTransform.localRotation = Quaternion.LookRotation(forward, Vector3.up);
        }

        RotateY();
        if (xPivotTransform != null) RotatePivotX();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var dir = Vector3.up;
            var strength = 15f;
            var force = dir * strength;
            rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        if (Application.isFocused) Move();
        ApplyGravity();
    }

    private void Move()
    {
        if (rigidbody == null) return;

        var dir = transform.TransformDirection(CustomInput.MultiAxis(axis: "Move XZ", adjust: true));
        var force = dir * inputSettings.pushForce;
        rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    private void ApplyGravity()
    {
        var dir = -Vector3.up;
        var strength = 20f;
        var force = dir * strength;
        rigidbody.AddForce(force, ForceMode.Acceleration);
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
            return inputAsset.pushForceStrength;
        }
    }

    public Vector2 xTurnRange = new(-0.8f, 0.9f);
}