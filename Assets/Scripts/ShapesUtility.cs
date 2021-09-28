using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShapesUtility : MonoBehaviour
{
    [SerializeField] private float radiusCircular;
    [SerializeField] private int n;
    [SerializeField] private int d = 2;
    [SerializeField] private bool penta;
    
    private void OnDrawGizmos()
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < n; i++)
        {
            float t = i / (float) n;
            points.Add(Circle(t));
        }
        
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawLine(points[i], points[(i + d) % points.Count]);
        }
    }
    
    private Vector3 Circle(float t)
    {
        float x = Mathf.Cos(t * Mathf.PI * 2) * radiusCircular;
        float y = Mathf.Sin(t * Mathf.PI * 2) * radiusCircular;
        return new Vector3(x, y, 0f);
    }
}
