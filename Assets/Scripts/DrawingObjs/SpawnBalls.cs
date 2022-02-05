using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float padding;
    
    void Start()
    {
        Draw();
    }

    private void Draw()
    {
        for (int i = 0; i < (int) EasingUtility.StyleCount; i++)
        {
            for (int j = 0; j < (int) EasingUtility.ModeCount; j++)
            {
                GameObject temp = Instantiate(prefab, transform);
                var offsetX = i * padding;
                var offsetY = -j * padding;
                temp.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY,
                    transform.position.z);

                DrawBall drawBall = temp.GetComponent<DrawBall>();
                drawBall.SetValues(temp.transform.position, temp.transform.position - Vector3.down, i, j, 2);
                StartCoroutine(drawBall.Draw());
            }
        }
    }
}
