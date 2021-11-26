using System.Collections.Generic;
using UnityEngine;

namespace Drawz
{
    public class DrawBouncee : DrawShapes
    {
        [HideInInspector] public List<float> x;
        [HideInInspector] public List<float> y;
        
        public override void Draw()
        {
            for (int i = 0; i < vertexCount; i++)
            {
                //x = i / (float) vertexCount;
                //y = EasingUtility.OutElastic(x);
                lineRenderer.SetPosition(i, new Vector3(x[i], y[i], 0) + transform.position);
                lineRenderer.sortingOrder = 3;
            }
        }
    }
}
