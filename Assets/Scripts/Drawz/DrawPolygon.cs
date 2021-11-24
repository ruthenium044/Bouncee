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
    }
}
