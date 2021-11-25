using UnityEngine;

namespace Drawz
{
    public class DrawBouncee : DrawShapes
    {
        public override void Draw()
        {
            float x, y;
            for (int i = 0; i < vertexCount; i++)
            {
                x = i / (float) vertexCount;
                y = EasingUtility.OutElastic(x);
                lineRenderer.SetPosition(i, new Vector3(x, y, 0) + transform.position);
            }
        }
    }
}
