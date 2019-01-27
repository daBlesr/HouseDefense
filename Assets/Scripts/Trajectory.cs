using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory
{
    public static Vector3[] Compute(float velocity, float rad, Vector3 position0, int nVertices, int direction)
    {
        float vSinTheta = velocity * Mathf.Sin(rad);
        float totalTime = (vSinTheta + Mathf.Sqrt(vSinTheta * vSinTheta + 2 * 9.8f * position0.y)) / 9.8f;
        float timestep = totalTime / nVertices;
        Vector3[] points = new Vector3[nVertices];

        for (int i = 0; i < nVertices; i++)
        {
            float t = i * timestep;
            points[i] = new Vector3(
                direction * velocity * t * Mathf.Cos(rad),
                velocity * t * Mathf.Sin(rad) - 0.5f * 9.8f * t * t,
                0
            ) + position0;
        }

        return points;
    }
}
