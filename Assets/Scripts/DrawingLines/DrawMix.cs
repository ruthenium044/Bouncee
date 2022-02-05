using UnityEngine;

public class DrawMix : Draw
{
    [SerializeField] private EasingUtility.Style style;
    [SerializeField] private EasingUtility.Style mixStyle;
    [SerializeField] private EasingUtility.Mode mode;
    
    public void Draw(DrawMix graph, int style, int mixStyle, int mode)
    {
        var function = EasingUtility.GetFunction((EasingUtility.Style) style, (EasingUtility.Mode) mode);
        var function2 = EasingUtility.GetFunction((EasingUtility.Style) mixStyle, (EasingUtility.Mode) mode);

        for (int i = 0; i < graph.drawData.VertexCount; i++)
        {
            float x = i / (float) graph.drawData.VertexCount;
            float y = function(x) * function2(x);
            graph.points[i] = new Vector2(x, y);
        }
        graph.SetPositions();
    }

    private void Start()
    {
        Draw(this, (int) style, (int) mixStyle, (int) mode);
    }
}
