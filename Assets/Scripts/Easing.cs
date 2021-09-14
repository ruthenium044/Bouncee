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
        EaseInOutSine,
        EaseInCircular,
        EaseOutCircular,
        EaseInOutCircular,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCub,
        EaseOutCub,
        EaseInOutCub,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint
    }
    [SerializeField] private EaseStyle easeStyle;

    delegate float EaseFunction(float a, float b, float t);
    private EaseFunction easeFunction;
    
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 end;

    void Start()
    {
        SetEase(easeFunction);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(EaseSelected(easeFunction, start, end));
        }
    }

    private IEnumerator EaseSelected(EaseFunction ease,Vector3 startPos, Vector3 endPos)
    {
        float t = 0;
        while (t < 1.0f)
        {
            transform.position = Ease(ease, startPos, endPos, t);
            t += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.position = endPos;
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

    float EaseInCircular(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }
    
    float EaseOutCircular(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }
    
    float EaseInOutCircular(float a, float b, float t)
    {
        return EaseLinear(a, b, t); 
    }
    
    float EaseInExpo(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }
    
    float EaseOutExpo(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }
    
    float EaseInOutExpo(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }
    
    float EaseInQuad(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseOutQuad(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseInOutQuad(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseInCub(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseOutCub(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }
    
    float EaseInOutCub(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseInQuart(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseOutQuart(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseInOutQuart(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseInQuint(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseOutQuint(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    float EaseInOutQuint(float a, float b, float t)
    {
        return EaseLinear(a, b, t);
    }

    
    //converters
    private float Ease(EaseFunction ease, float a, float b, float t)
    {
        return ease(a, b, t);
    }
    
    private Vector2 Ease(EaseFunction ease, Vector2 a, Vector2 b, float t)
    {
        return new Vector2(ease(a.x, b.x, t), ease(a.y, b.y, t));
    }
    
    private Vector3 Ease(EaseFunction ease,Vector3 a, Vector3 b, float t)
    {
        return new Vector3(ease(a.x, b.x, t), ease(a.y, b.y, t), ease(a.z, b.z, t));
    }

    private EaseFunction SetEase(EaseFunction ease)
    {
        switch (easeStyle)
        {
            case EaseStyle.EaseLinear:
                ease = EaseLinear;
                break;
            case EaseStyle.EaseInSine:
                ease = EaseInSine;
                break;
            case EaseStyle.EaseOutSine:
                ease = EaseOutSine;
                break;
            case EaseStyle.EaseInOutSine:
                ease = EaseInOutSine;
                break;
            case EaseStyle.EaseInCircular:
                break;
            case EaseStyle.EaseOutCircular:
                break;
            case EaseStyle.EaseInOutCircular:
                break;
            case EaseStyle.EaseInExpo:
                break;
            case EaseStyle.EaseOutExpo:
                break;
            case EaseStyle.EaseInOutExpo:
                break;
            case EaseStyle.EaseInQuad:
                break;
            case EaseStyle.EaseOutQuad:
                break;
            case EaseStyle.EaseInOutQuad:
                break;
            case EaseStyle.EaseInCub:
                break;
            case EaseStyle.EaseOutCub:
                break;
            case EaseStyle.EaseInOutCub:
                break;
            case EaseStyle.EaseInQuart:
                break;
            case EaseStyle.EaseOutQuart:
                break;
            case EaseStyle.EaseInOutQuart:
                break;
            case EaseStyle.EaseInQuint:
                break;
            case EaseStyle.EaseOutQuint:
                break;
            case EaseStyle.EaseInOutQuint:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return ease;
    }
}
