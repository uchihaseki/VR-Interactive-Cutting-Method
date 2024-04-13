using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLinecast : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public int numPoints = 50; // Number of points to sample along the curve

    private void Update()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / (numPoints - 1);
            Vector3 pointOnCurve = BezierCurve(startPoint.position, Vector3.up * 5f, endPoint.position, t);

            RaycastHit hit;
            if (Physics.Raycast(startPoint.position, pointOnCurve - startPoint.position, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(startPoint.position, hit.point, Color.red);
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawLine(startPoint.position, pointOnCurve, Color.green);
                Debug.Log("No collision at point " + i);
            }
        }
    }

    // Simple Bezier curve function
    private Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0;
        point += 2 * u * t * p1;
        point += tt * p2;

        return point;
    }
}




