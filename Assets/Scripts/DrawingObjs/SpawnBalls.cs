using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float paddingX;
    [SerializeField] private float paddingY;
    
    void Start()
    {
        Draw();
    }

    private void Draw()
    {
        for (int i = 0; i < EasingUtility.StyleCount; i++)
        {
            for (int j = 0; j < EasingUtility.ModeCount; j++)
            {
                GameObject temp = Instantiate(prefab, transform);
                var offsetX = i * paddingX;
                var offsetY = -j * paddingY;
                temp.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY,
                    transform.position.z);

                DrawBall drawBall = temp.GetComponent<DrawBall>();
                drawBall.SetValues(temp.transform.position, 
                    temp.transform.position - Vector3.down, 
                                        i, j, 2);
                StartCoroutine(drawBall.Draw());
            }
        }
    }
}
