using System.Collections.Generic;
using UnityEngine;

namespace Drawz
{
    public class DrawPolygon : DrawShapes
    {
        [SerializeField] private float radius;

        private void Start()
        {
            Initialize();
            Draw();
        }

        public override void Draw()
        {
            float angle = 2 * Mathf.PI / vertexCount;
            for (int i = 0; i < vertexCount; i++)
            {
                Matrix4x4 rotationMatrix = new Matrix4x4(
                    new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                    new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                    new Vector4(0, 0, 1, 0),
                    new Vector4(0, 0, 0, 1));
                Vector3 initialRelativePosition = new Vector3(0, radius, 0);
                lineRenderer.SetPosition(i, transform.position + rotationMatrix.MultiplyPoint(initialRelativePosition));
            }
        }

        private void Draw2()
        {
            List<Vector3> points = new List<Vector3>();
            for (int i = 0; i < vertexCount; i++)
            {
                float t = i / (float) vertexCount;
                points.Add(Circle(t));
            }
            List<Vector3> pointsToDraw = new List<Vector3>();
            for (int i = 0; i < points.Count; i++)
            {
                //Gizmos.DrawLine(points[i], points[(i + d) % points.Count]);
            }
        }
        
        private Vector3 Circle(float t)
        {
            float x = Mathf.Cos(t * Mathf.PI * 2) * radius;
            float y = Mathf.Sin(t * Mathf.PI * 2) * radius;
            return new Vector3(x, y, 0f);
        }
    }
}
