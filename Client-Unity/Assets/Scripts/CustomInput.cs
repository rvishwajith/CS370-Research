using UnityEngine;

/// <summary>
/// Get abstracted input values for values such as mouse input, horizontal movement, etc. Values can be adjusted for sensitivity/normalization, etc. if needed.
/// </summary>
public static class CustomInput
{
    public static float SingleAxis(string axis)
    {
        switch (axis)
        {
            case "Mouse X":
                return Input.GetAxis("Mouse X");
            case "Mouse Y":
                return Input.GetAxis("Mouse Y");
            default:
                return 0;
        }
    }

    public static KeyCode MOVE_FRONT = KeyCode.W, MOVE_BACK = KeyCode.S, MOVE_RIGHT = KeyCode.D, MOVE_LEFT = KeyCode.A;

    public static Vector3 MultiAxis(string axis, bool adjust = true)
    {
        Vector3 MouseXY()
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            if (adjust)
                return new Vector3(y, x);
            else
                return new Vector3(x, y);
        }

        Vector3 MoveXZ()
        {
            float x = 0, z = 0;
            if (Input.GetKey(MOVE_RIGHT))
                x = 1;
            else if (Input.GetKey(MOVE_LEFT))
                x = -1;
            if (Input.GetKey(MOVE_FRONT))
                z = 1;
            else if (Input.GetKey(MOVE_BACK))
                z = -1;
            if (adjust)
                return new Vector3(x, 0, z).normalized;
            else
                return new Vector3(x, 0, z);
        }

        switch (axis)
        {
            case "Mouse XY":
                return MouseXY();
            case "Move XZ":
                return MoveXZ();
            default:
                return Vector3.zero;
        }
    }

    public static Vector3 MouseInput(bool clampMagnitude = false, bool useDeltaTime = false)
    {
        // X and Y are intentionally reversed.
        var x = Input.GetAxis("Mouse Y");
        var y = Input.GetAxis("Mouse X");
        var result = new Vector3(x, y);
        if (clampMagnitude)
            result = Vector3.ClampMagnitude(result, 1);
        if (useDeltaTime)
            result *= Time.deltaTime;
        return result;
    }
}