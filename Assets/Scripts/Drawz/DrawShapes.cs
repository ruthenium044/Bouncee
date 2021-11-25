using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drawz
{
    public abstract class DrawShapes : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private Vector2 lineSize;
        [SerializeField] protected int vertexCount;
        [SerializeField] private bool loop;
        protected LineRenderer lineRenderer;

        public virtual void Start()
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(material);
            lineRenderer.startWidth = lineSize.x;
            lineRenderer.endWidth = lineSize.y;
            lineRenderer.loop = loop;
            lineRenderer.positionCount = vertexCount;
        }
  
        public virtual void  Update()
        {
            Draw();
        }
        
        public abstract void Draw();
    }
}