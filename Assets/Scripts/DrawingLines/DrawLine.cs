using UnityEngine;

public class DrawLine : Draw
{
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    private void Draw(DrawLine graph)
    {
        graph.points[0] = startPosition;
        graph.points[1] = endPosition;
        graph.SetPositions();
    }

    private void Start()
    {
        Draw(this);
    }
}
