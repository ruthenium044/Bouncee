using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    
    void Start()
    {
        float offsetX = 5.5f;
        float offsetY = 0;
        float paddingY = 4;
        for (int x = 0; x < 12; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                GameObject temp = Instantiate(ballPrefab, transform);
                temp.transform.position =
                    new Vector3(transform.position.x + x - offsetX, transform.position.y + y * paddingY - offsetY, transform.position.z);
                float num = y * paddingY - offsetY;
                temp.GetComponent<Easing>().Start1 = num;
                temp.GetComponent<Easing>().End = num - 4;
                temp.GetComponent<Easing>().SetEase((Easing.EaseStyle) (x * 2 + y));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
