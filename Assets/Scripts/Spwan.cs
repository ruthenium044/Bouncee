using System.Collections;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float padding;

    void Start()
    {
        for (int i = 0; i < (int) EasingUtility.Style.Count; i++)
        {
            for (int j = 0; j < (int) EasingUtility.Mode.Count; j++)
            {
                GameObject temp = Instantiate(prefab, transform);
                var offsetX = i * padding;
                var offsetY = - j * padding;
                temp.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z);
                
                DrawGraph drawGraph = temp.GetComponent<DrawGraph>();
                drawGraph.Draw(drawGraph, i, j);
            }
        }
    }
}
