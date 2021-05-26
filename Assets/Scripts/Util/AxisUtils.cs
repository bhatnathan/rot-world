using UnityEngine;

public class AxisUtils : MonoBehaviour
{
    public static Vector3 AxisToVector(Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                return Vector3.right;

            case Axis.Y:
                return Vector3.up;

            case Axis.Z:
                return Vector3.forward;

            default:
                return Vector3.zero;
        }
    }

}
