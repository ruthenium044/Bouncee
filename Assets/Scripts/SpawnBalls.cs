using System;
using System.Collections;
using Drawz;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    
    void Awake()
    {
        float offsetX = 0;
        float offsetY = 0;
        float padding = 3;
        for (int i = 0; i < (int) EasingUtility.Style.Count; i++)
        {
            for (int j = 0; j < (int) EasingUtility.Mode.Count; j++)
            {
                GameObject temp = Instantiate(ballPrefab, transform);
                offsetX = i * padding;
                offsetY = j * padding;
                temp.transform.position = new Vector3(transform.position.x + offsetX, 
                                                      transform.position.y - offsetY, 
                                                        transform.position.z);
                
                var function = EasingUtility.GetFunction((EasingUtility.Style) i, (EasingUtility.Mode) j);
                DrawBouncee bounce = temp.GetComponent<DrawBouncee>();
                
                StartCoroutine(ease(function, bounce));
            }
        }
    }

    IEnumerator ease(EasingUtility.Function func, DrawBouncee bounce) 
    {
        for (int i = 0; i < bounce.vertexCount; i++)
        {
            bounce.x.Add(i / (float) bounce.vertexCount);
            bounce.y.Add(func(bounce.x[i]) / 4);
        }
        yield return null;
    }
    


}
