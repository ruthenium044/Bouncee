using UnityEngine;

[CreateAssetMenu]

public class DrawData : ScriptableObject
{
    [SerializeField] private Vector2 lineSize;
    [SerializeField] private int vertexCount;
    [SerializeField] private Material material;
    [SerializeField] private Color color;
    
    public Vector2 LineSize => lineSize;
    
    public int VertexCount => vertexCount;

    public Material Material => material;

    public Color Color => color;
}
