using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    private void ChangeEase(Vector2 pos, float t)
    {
        if (t > 0.5)
        {
            ease.SetEase(Easing.EaseStyle.EaseInQuad);
        }
    }
    
    Easing ease = new Easing();
    void Start()
    {
        float offsetX = -12f;
        float offsetY = 2;
        int width = 12;
        int paddingX = 2; //todo how to make this padding every 3rd. 
        int paddingY = -4;
        
        ease.Duration = 2.0f;
        ease.SetEase(Easing.EaseStyle.EaseLinear);
        Rigidbody rb;
        
        for (int i = 2; i < (int) Easing.EaseStyle.Count + 2; i++)
        {
            GameObject temp = Instantiate(ballPrefab, transform);
            float tempX = ((transform.position.x) + i % width) * paddingX + offsetX;
            float tempY = i / width * paddingY + offsetY;
            
            temp.transform.position = new Vector3(tempX,  tempY, transform.position.z);
            var function = temp.GetComponent<Easing>().SetEase(Easing.EaseStyle.EaseLinear);
            temp.GetComponent<Easing>().StartValue = new Vector2(tempX, tempY);
            temp.GetComponent<Easing>().EndValue =  new Vector2(tempX + 1,tempY + 1);
            temp.GetComponent<Easing>().SetEase((Easing.EaseStyle) (i - 2));
        }
    }

}
