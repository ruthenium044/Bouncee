using System.Collections;
using UnityEngine;

public class DrawAllGraphs : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float paddingX;
    [SerializeField] private float paddingY;
    [SerializeField] private bool vertical = false;
    
    void Start()
    {
        DrawGraphs();
    }

    private void DrawGraphs()
    {
        for (int i = 0; i < (vertical ? (int) EasingUtility.StyleCount :  (int) EasingUtility.ModeCount); i++)
        {
            for (int j = 0; j < (vertical ? (int) EasingUtility.ModeCount : (int) EasingUtility.StyleCount); j++)
            {
                GameObject temp = Instantiate(prefab, transform);
                var offsetX = i * paddingX;
                var offsetY = -j * paddingY;
                temp.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY,
                    transform.position.z);

                DrawGraph drawGraph = temp.GetComponent<DrawGraph>();
                drawGraph.drawItself = false;
                drawGraph.Draw(drawGraph, (vertical ? i : j), (vertical ? j : i));
            }
        }
    }
}
