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


        for (int i = 0; i < (int) EasingUtility.Style.Count; i++)
        {
            for (int j = 0; j < (int) EasingUtility.Mode.Count; j++)
            {
                GameObject temp = Instantiate(ballPrefab, transform);
                float tempX = ((transform.position.x) + i % width) * paddingX + offsetX;
                float tempY = i / width * paddingY + offsetY;
                tempY += j * paddingY;

                temp.transform.position = new Vector3(tempX, tempY, transform.position.z);
                var function = EasingUtility.GetFunction((EasingUtility.Style) i, (EasingUtility.Mode) j);
                StartCoroutine(ease(temp.transform, function, new Vector2(tempX, tempY),
                    new Vector2(tempX + 4, tempY + 4)));
            }
        }
    }

    IEnumerator ease(Transform that, EasingUtility.Function func, Vector2 start, Vector2 end) 
    {
        float t = 0;

        while(t <= 1.0f) { //this whole lower kinda confusing tbh
            Vector2 next = that.position;
            var t1 = EasingUtility.GetFunction(EasingUtility.Style.Linear, EasingUtility.Mode.In)(t);
            next.x = EasingUtility.Interpolate(start.x, end.x, t1);

            //weighted average here. disable if want normal values
            //todo for some reson this fucks with spacing fix it
            //next.x = Easing.WeightedAverage(start.x, t, slow);
            var t2 = func(t);
            next.y = EasingUtility.Interpolate(start.y, end.y, t2);
            that.position = next;
            t += Time.deltaTime;
            yield return null;
		}
        that.position = end;

        StartCoroutine(ease(that, func, start, end));
	}
    


}
