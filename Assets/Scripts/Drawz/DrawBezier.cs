using UnityEngine;

namespace Drawz
{
    public class DrawBezier : DrawShapes
    {
        [SerializeField] private Transform startPos;
        [SerializeField] private Transform endPos;
        [SerializeField] private Transform tangent;
        
        public override void Draw()
        {
            Vector3 pos0 = startPos.position;
            Vector3 pos1 = endPos.position;
            Vector3 tan0 = tangent.position;
            float t = 0;
            Vector3 B = Vector3.zero;
            for (int i = 0; i < vertexCount; i++)
            {
                B = (1 - t) * (1 - t) * pos0 + 2 * (1 - t) * t * tan0 + t * t * pos1;
                lineRenderer.SetPosition(i, B);
                t += (1 / (float)vertexCount);
            }
        }
    }
}
