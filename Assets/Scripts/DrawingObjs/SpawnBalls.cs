using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float paddingX;
    [SerializeField] private float paddingY;
    [SerializeField] private bool vertical = false;
    
    void Start()
    {
        Draw();
    }

    private void Draw()
    {
        for (int i = 0; i < (vertical ? EasingUtility.StyleCount : EasingUtility.ModeCount); i++)
        {
            for (int j = 0; j < (vertical ? EasingUtility.ModeCount : EasingUtility.StyleCount); j++)
            {
                GameObject temp = Instantiate(prefab, transform);
                var offsetX = i * paddingX;
                var offsetY = -j * paddingY;
                temp.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY,
                    transform.position.z);

                DrawBall drawBall = temp.GetComponent<DrawBall>();
                drawBall.SetValues(temp.transform.position, 
                    temp.transform.position - Vector3.down, 
                    (vertical ? i : j), (vertical ? j : i), 1);
                StartCoroutine(drawBall.Draw());
            }
        }
    }
}
