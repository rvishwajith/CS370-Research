using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Player Input Asset")]
public class PlayerInputAsset : ScriptableObject
{
    public float sensMultiplier = 5;
    public float pushForceMultiplier = 30;
}

