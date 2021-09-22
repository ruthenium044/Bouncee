using System.Collections;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private float slow = 2f;
    
    void Start()
    {
        float offsetX = -12f;
        float offsetY = 2;
        int width = 12;
        int paddingX = 5; //todo how to make this padding every 4th. 
        int paddingY = -6;

        for (int i = 0; i < (int) Easing.Style.Count; i++)
        {
            GameObject temp = Instantiate(ballPrefab, transform);
            float tempX = ((transform.position.x) + i % width) * paddingX + offsetX;
            float tempY = i / width * paddingY + offsetY;

            temp.transform.position = new Vector3(tempX,  tempY, transform.position.z);
            var function = Easing.GetFunction((Easing.Style)i);
            StartCoroutine(ease(temp.transform, function, new Vector2(tempX, tempY), new Vector2(tempX + 4, tempY + 4)));
        }
    }

    IEnumerator ease(Transform that, Easing.Function func, Vector2 start, Vector2 end) 
    {
        float t = 0;

        while(t <= 1.0f) { //this whole lower kinda confusing tbh
            Vector2 next = that.position;
            var t1 = Easing.GetFunction(Easing.Style.Linear)(t);
            next.x = Easing.Interpolate(start.x, end.x, t1);

            //weighted average here. disable if want normal values
            //todo for some reson this fucks with spacing fix it
            //next.x = Easing.WeightedAverage(start.x, t, slow);
            var t2 = func(t);
            next.y = Easing.Interpolate(start.y, end.y, t2);
            that.position = next;
            t += Time.deltaTime;
            yield return null;
		}
        that.position = end;

        StartCoroutine(ease(that, func, start, end));
	}
    


}
