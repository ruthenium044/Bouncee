using UnityEngine;

namespace Drawz
{
    public class DrawWave : DrawShapes
    {
        [SerializeField] private float amplitude;
        [SerializeField] private float wavelength;
        
        public override void Draw()
        {
            float x = 0f;
            float y;
            float k = 2 * Mathf.PI / wavelength;
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                x += i * 0.001f;
                y = amplitude * Mathf.Sin(k * x);
                lineRenderer.SetPosition(i, new Vector3(x, y, 0) + transform.position);
            }
        }
    }
}
