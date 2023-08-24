using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Player Input Asset")]
public class PlayerInputAsset : ScriptableObject
{
    public float sensMultiplier = default;
    public float pushForceMultiplier = default;
}

