using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    
    void Start()
    {
        float offsetX = 10.5f;

        for (int x = 0; x < 21; x++)
        {
            GameObject temp = Instantiate(ballPrefab, transform);
            temp.transform.position =
                new Vector3(transform.position.x + x - offsetX, -2, transform.position.z);
            temp.GetComponent<Easing>().StartValue = -2;
            temp.GetComponent<Easing>().EndValue = 2;
            temp.GetComponent<Easing>().SetEase((Easing.EaseStyle) (x));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
