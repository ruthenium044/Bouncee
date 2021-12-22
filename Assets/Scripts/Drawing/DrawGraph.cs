using UnityEngine;

public class DrawGraph : Draw
{
    [SerializeField] private EasingUtility.Style style;
    [SerializeField] private EasingUtility.Mode mode;
    [HideInInspector] public bool drawItself = true; 
    
    public void Draw(DrawGraph graph, int style, int mode)
    {
        var function = EasingUtility.GetFunction((EasingUtility.Style) style, (EasingUtility.Mode) mode);
        for (int i = 0; i < graph.drawData.VertexCount; i++)
        {
            float x = i / (float) graph.drawData.VertexCount;
            float y = function(x);
            graph.points[i] = new Vector2(x, y);
        }
        graph.SetPositions();
    }

    private void Start()
    {
        if (drawItself)
        {
            Draw(this, (int) style, (int) mode);
        }
    }
}
