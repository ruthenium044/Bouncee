using System;
using System.Collections;
using UnityEngine;

public class Easing : MonoBehaviour
{
    private enum EaseStyle
    {
        EaseLinear,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine
    }
    [SerializeField] private EaseStyle easeStyle;

    delegate void EaseFunction(float a, float b, float t);
    
    [SerializeField] private float start;
    [SerializeField] private float end;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            StartCoroutine(Ease(start, end));
        }
    }

    private IEnumerator Ease(float startPos, float endPos)
    {
        float t = 0;
        while (t < 1.0f)
        { //here call function acording to the ease style somehow
            float ease = EaseInOutSine(startPos, endPos, t);
            transform.position = new Vector3(transform.position.x, ease, transform.position.z);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, endPos, transform.position.z);;
    }

    float EaseLinear(float a, float b, float t)
    {
        return a * (1 - t) + b * t;
    }
    
    float EaseInSine(float a, float b, float t)
    {
        t = 1 - Mathf.Cos(t * Mathf.PI / 2);
        return EaseLinear(a, b, t);
    }
    
    float EaseOutSine(float a, float b, float t)
    {
        t = Mathf.Sin(t * Mathf.PI / 2);
        return EaseLinear(a, b, t);
    }

    float EaseInOutSine(float a, float b, float t)
    {
        t = (1 + Mathf.Sin(((t - 0.5f) * Mathf.PI))) / 2;
        return EaseLinear(a, b, t);
    }
    
    //converter lates
    //Vector2 EaseVector2(Vector2 a, Vector2 b, float t)
    //{
    //    return new Vector2(EaseIn(a.x, b.x, t), EaseIn(a.y, b.y, t));
    //}
}
