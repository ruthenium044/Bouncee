using UnityEngine;

namespace Drawz
{
    public class DrawLine : DrawShapes
    {
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 endPosition;

        public override void Draw()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPosition + transform.position);
            lineRenderer.SetPosition(1, endPosition + transform.position);
        }
    }
}
