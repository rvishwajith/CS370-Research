using UnityEngine;

public static class DebugProperties
{
    public static readonly GUIStyle BlackOnYellowStyle;

    static DebugProperties()
    {
        var yellowBackground = Texture2D.blackTexture;
        // yellowBackground.SetPixel(0, 0, Color.yellow);

        BlackOnYellowStyle = new();
        BlackOnYellowStyle.normal = GUI.skin.label.normal;
        BlackOnYellowStyle.normal.background = yellowBackground;
    }
}