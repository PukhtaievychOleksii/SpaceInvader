using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorHelper 
{
    public static Vector3 RotateVector(Vector3 vector,float angle)
    {
        Quaternion quaternion = Quaternion.EulerAngles(0, 0, angle);
        return quaternion * vector;
    }
}
