using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMix : MonoBehaviour
{
    [SerializeField] private Vector2 lineSize;
    [SerializeField] public int vertexCount;
    [SerializeField] private Material material;
    private  LineRenderer lineRenderer;

    [HideInInspector] public List<float> x;
    [HideInInspector] public List<float> y;
    
    [SerializeField] private EasingUtility.Style style;
    [SerializeField] private EasingUtility.Style mixStyle;
    [SerializeField] private EasingUtility.Mode mode;

    void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.startWidth = lineSize.x;
        lineRenderer.endWidth = lineSize.y;
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = vertexCount;
        lineRenderer.loop = false;
    }
    
    public void Draw(DrawMix graph, int style, int mixStyle, int mode)
    {
        var function = EasingUtility.GetFunction((EasingUtility.Style) style, (EasingUtility.Mode) mode);
        var function2 = EasingUtility.GetFunction((EasingUtility.Style) mixStyle, (EasingUtility.Mode) mode);

        for (int i = 0; i < graph.vertexCount; i++)
        {
            graph.x.Add(i / (float) graph.vertexCount);
            var t = function(graph.x[i]) * function2(graph.x[i]);
            graph.y.Add(t);
        }
        graph.SetPositions();
    }
    
    public void SetPositions()
    {
        for (int i = 0; i < vertexCount; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(x[i], y[i], 0) + transform.position);
        }
    }

    private void Update()
    {
        Draw(this, (int) style, (int) mixStyle, (int) mode);
    }
}
