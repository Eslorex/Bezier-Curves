using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    public static Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }

    public static Vector3 CalculateControlPoint(Vector3 Position, Vector3 targetPosition, float curveFactor)
    {
        return (Position + targetPosition) / 2f + Vector3.up * (targetPosition - Position).magnitude * curveFactor;
    }
}

