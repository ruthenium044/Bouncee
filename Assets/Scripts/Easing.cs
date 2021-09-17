using System;
using UnityEngine;
using System.Collections;

public static class Easing 
{
    public delegate float Function(float a, float b, float t);

    public enum Style
    {
        Linear,
        InSine,
        OutSine,
        InOutSine,
        InCircular,
        OutCircular,
        InOutCircular,
        InExpo,
        OutExpo,
        InOutExpo,
        InQuad,
        OutQuad,
        InOutQuad,
        InCub,
        OutCub,
        InOutCub,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        Count
    }

    //i want ease to be set with enum (for loops)
    public static Function GetFunction(Style style)
    {
        switch (style)
        {
            case Style.Linear:
                return Linear;
            case Style.InSine:
                return InSine;
            case Style.OutSine:
                return OutSine;
            case Style.InOutSine:
                return InOutSine;
            case Style.InCircular:
                return InCircular;
            case Style.OutCircular:
                return OutCircular;
            case Style.InOutCircular:
                return InOutCircular;
            case Style.InExpo:
                return InExpo;
            case Style.OutExpo:
                return OutExpo;
            case Style.InOutExpo:
                return InOutExpo;
            case Style.InQuad:
                return InQuad;
            case Style.OutQuad:
                return OutQuad;
            case Style.InOutQuad:
                return InOutQuad;
            case Style.InCub:
                return InCub;
            case Style.OutCub:
                return OutCub;
            case Style.InOutCub:
                return InOutCub;
            case Style.InQuart:
                return InQuart;
            case Style.OutQuart:
                return OutQuart;
            case Style.InOutQuart:
                return InOutQuart;
            case Style.InQuint:
                return InQuint;
            case Style.OutQuint:
                return OutQuint;
            case Style.InOutQuint:
                return InOutQuint;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //converters
    public static float Ease(Function ease, float a, float b, float t)
    {
        return ease(a, b, t);
    }

    public static Vector2 Ease(Function ease, Vector2 a, Vector2 b, float t)
    {
        return new Vector2(ease(a.x, b.x, t), ease(a.y, b.y, t));
    }

    public static Vector3 Ease(Function ease,Vector3 a, Vector3 b, float t)
    {
        return new Vector3(ease(a.x, b.x, t), ease(a.y, b.y, t), ease(a.z, b.z, t));
    }

    //the easings
    public static float Linear(float a, float b, float t)
    {
        return a * (1 - t) + b * t;
    }

    public static float InSine(float a, float b, float t)
    {
        t = 1 - Mathf.Cos(t * Mathf.PI / 2);
        return Linear(a, b, t);
    }

    public static float OutSine(float a, float b, float t)
    {
        t = Mathf.Sin(t * Mathf.PI / 2);
        return Linear(a, b, t);
    }

    public static float InOutSine(float a, float b, float t)
    {
        t = (1 + Mathf.Sin(((t - 0.5f) * Mathf.PI))) / 2;
        return Linear(a, b, t);
    }

    public static float InCircular(float a, float b, float t)
    {
        t = 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
        return Linear(a, b, t);
    }

    public static float OutCircular(float a, float b, float t)
    {
        t = Mathf.Sqrt(1 - Mathf.Pow((t - 1), 2));
        return Linear(a, b, t);
    }

    public static float InOutCircular(float a, float b, float t)
    {
        t = t < 0.5f ? 0.5f - Mathf.Sqrt(0.25f - t * t) : Mathf.Sqrt(0.25f - Mathf.Pow(t - 1, 2)) + 0.5f;
        return Linear(a, b, t);
    }

    public static float InExpo(float a, float b, float t)
    {
        t = 1 - Mathf.Sqrt(1 - t);
        return Linear(a, b, t);
    }

    public static float OutExpo(float a, float b, float t)
    {
        t = Mathf.Sqrt(t);
        return Linear(a, b, t);
    }

    public static float InOutExpo(float a, float b, float t)
    {
        t = t <= 0.5f ? 0.5f - Mathf.Sqrt(1 - 2 * t) / 2 : Mathf.Sqrt(2 * t - 1) / 2 + 0.5f;
        return Linear(a, b, t);
    }

    public static float InQuad(float a, float b, float t)
    {
        t = Mathf.Pow(t, 2);
        return Linear(a, b, t);
    }

    public static float OutQuad(float a, float b, float t)
    {
        t = 1 - Mathf.Pow(t - 1, 2);
        return Linear(a, b, t);
    }

    public static float InOutQuad(float a, float b, float t)
    {
        t = t <= 0.5f ? 2 * t * t : 1 - Mathf.Pow(2 * t - 2, 2) / 2;
        return Linear(a, b, t);
    }

    public static float InCub(float a, float b, float t)
    {
        t = Mathf.Pow(t, 3);
        return Linear(a, b, t);
    }

    public static float OutCub(float a, float b, float t)
    {
        t = Mathf.Pow( t - 1, 3) + 1;
        return Linear(a, b, t);
    }

    public static float InOutCub(float a, float b, float t)
    {
        t = t <= 0.5 ? 4 * Mathf.Pow(t, 3) : 4 * Mathf.Pow(t - 1, 3) + 1;
        return Linear(a, b, t);
    }

    public static float InQuart(float a, float b, float t)
    {
        t = Mathf.Pow(t, 4);
        return Linear(a, b, t);
    }

    public static float OutQuart(float a, float b, float t)
    {
        t = 1 - Mathf.Pow(t - 1, 4);
        return Linear(a, b, t);
    }

    public static float InOutQuart(float a, float b, float t)
    {
        t = t <= 0.5f ? - 8 * Mathf.Pow(t - 0.5f, 4) + 0.5f : 8 * Mathf.Pow(t - 0.5f, 4) + 0.5f;
        return Linear(a, b, t);
    }

    public static float InQuint(float a, float b, float t)
    {
        t = Mathf.Pow( t, 5);
        return Linear(a, b, t);
    }

    public static float OutQuint(float a, float b, float t)
    {
        t = Mathf.Pow(t - 1, 5) + 1;
        return Linear(a, b, t);
    }

    public static float InOutQuint(float a, float b, float t)
    {
        t = t <= 0.5f ? 16 * Mathf.Pow(t, 5) : 16 * Mathf.Pow(t - 1, 5) + 1;
        return Linear(a, b, t);
    }
}