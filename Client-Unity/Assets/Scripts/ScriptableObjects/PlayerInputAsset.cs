using System;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Assets/Player Input Asset")]
public class PlayerInputAsset : ScriptableObject
{
    [Header("Controls")]

    [Header("The amount to multiply the user's mouse input sensitivity.")]
    public float sensMultiplier = 5;

    [Header("Physics & Movement")]

    [InfoBox("The amount of force to apply to the player for horizontal movement.")]
    public float pushForceStrength = 30;

    [InfoBox("The max movement speed the player can go, without limiting their fall speed.")]
    public float maxHorizontalLinearVelocity = 20;
    public bool clampHorizontalLinearVelocity = true;
}

