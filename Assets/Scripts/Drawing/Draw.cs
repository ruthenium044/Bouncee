using System;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] protected DrawData drawData;
    [HideInInspector] public Vector2[] points;

    private LineRenderer lineRenderer;
    private static readonly int ShaderColor = Shader.PropertyToID("MainColor");
    
    private MaterialPropertyBlock mbp;
    private MaterialPropertyBlock Mbp
    {
        get
        {
            if (mbp == null)
            {
                mbp = new MaterialPropertyBlock();
            }
            return mbp;
        }
    }
    
    void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.loop = false;

        SetData();
        SetColor();
    }
    
    protected void SetPositions()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0) + transform.position);
        }
    }
    
    private void SetData()
    {
        lineRenderer.startWidth = drawData.LineSize.x;
        lineRenderer.endWidth = drawData.LineSize.y;
        lineRenderer.material = drawData.Material;

        points = new Vector2[drawData.VertexCount];
        lineRenderer.positionCount = drawData.VertexCount;
    }

    private void SetColor()
    {
        Mbp.SetColor(ShaderColor, drawData.Color);
        lineRenderer.SetPropertyBlock(Mbp);
    }
}
