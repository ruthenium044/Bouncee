using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpiralUtility : MonoBehaviour
{
    [SerializeField] private float radius = 1;
    [SerializeField] private float radiusCircular = 1;
    [SerializeField] private int period = 1;
    [SerializeField] private int detailLevel = 36;
    [SerializeField] private bool circular;
    [SerializeField] private float lineThickness = 6f;
    [Header("Bezie")] 
    [SerializeField] private Transform starPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform startTangent;
    [SerializeField] private Transform endTangent;
    
    private void OnDrawGizmos()
    {
        float detailCount = detailLevel * period;

        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < detailCount; i++)
        {
            float t = i / detailCount; //normalize t
            points.Add(circular ? SpiralCircular(t) : Spiral(t));
        }

        //draw the lines one by one
        for (int i = 0; i < points.Count - 1; i++)
        {
            float t = i / detailCount;
            Handles.color = Color.Lerp(Color.green, Color.blue + Color.red, t);
            Handles.DrawAAPolyLine(lineThickness, points[i], points[i + 1]);
        }
        
        List<Vector3> circlePoints = new List<Vector3>();
        for (int i = 0; i < detailCount; i++)
        {
            float t = i / detailCount; //normalize t
            circlePoints.Add(Circle(t));
        }

        Handles.DrawAAPolyLine(circlePoints.ToArray());

        Handles.DrawBezier(starPoint.position, endPoint.position, 
            startTangent.position, endTangent.position, Color.green, null, 2f);
    }

    private Vector3 Circle(float t)
    {
        float x = Mathf.Cos(t * Mathf.PI * 2) * radiusCircular;
        float z = Mathf.Sin(t * Mathf.PI * 2) * radiusCircular;
        return new Vector3(x, 0f, z);
    }
    
    private Vector3 Spiral(float t)
    {
        Vector3 dir = (endPoint.position - starPoint.position).normalized;
        float x = EaseCos(t * Mathf.PI * 2 * period) + Mathf.LerpUnclamped(starPoint.position.x, endPoint.position.x, t);
        float y = EaseSin(t * Mathf.PI * 2 * period) + Mathf.LerpUnclamped(starPoint.position.y, endPoint.position.y, t);
        float z = Mathf.LerpUnclamped(starPoint.position.z, endPoint.position.z, t);

        Vector3 curve = new Vector3(x, y, z);   
        Gizmos.DrawLine(endPoint.transform.up, Vector3.zero);
        dir = Quaternion.AngleAxis(90, dir) * dir;
        Gizmos.DrawLine(dir, Vector3.zero);
        
        return curve ;
    }
    
    private Vector3 SpiralCircular(float t)
    {
        float x = EaseCos(t * Mathf.PI * 2 * period);
        float y = EaseSin(t * Mathf.PI * 2 * period);
        float z = Mathf.LerpUnclamped(0, 1, t);
        return new Vector3(x, y, z);
    }
    
    private float EaseSin(float t)
    {
        return radius * Mathf.Sin(t);
    }

    private float EaseCos(float t)
    {
        return radius * Mathf.Cos(t);
    }
}
