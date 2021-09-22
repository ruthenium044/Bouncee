using System;
using UnityEngine;
using System.Collections;

public static class Easing
{
    public delegate float Function(float t);

    public enum Style
    {
        Linear,
        SpikeLinear,
        InSine,
        OutSine,
        InOutSine,
        SpikeSine,
        InCircular,
        OutCircular,
        InOutCircular,
        SpikeCircular,
        InExpo,
        OutExpo,
        InOutExpo,
        SpikeExpo,
        InQuad,
        OutQuad,
        InOutQuad,
        SpikeQuad,
        InCub,
        OutCub,
        InOutCub,
        SpikeCub,
        InQuart,
        OutQuart,
        InOutQuart,
        SpikeQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        SpikeQuint,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce,
        SpikeBounce,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        SpikeElastic,
        EaseInBack,
        EaseOutBack,
        EaseInOutBack,
        SpikeBack,
        Count
    }

    //i want ease to be set with enum (for loops)
    public static Function GetFunction(Style style)
    {
        switch (style)
        {
            case Style.Linear:
                return Linear;
            case Style.SpikeLinear:
                return SpikeLinear;
            case Style.InSine:
                return InSine;
            case Style.OutSine:
                return OutSine;
            case Style.InOutSine:
                return InOutSine;
            case Style.SpikeSine:
                return SpikeSine;
            case Style.InCircular:
                return InCircular;
            case Style.OutCircular:
                return OutCircular;
            case Style.InOutCircular:
                return InOutCircular;
            case Style.SpikeCircular:
                return SpikeCircular;
            case Style.InExpo:
                return InExpo;
            case Style.OutExpo:
                return OutExpo;
            case Style.InOutExpo:
                return InOutExpo;
            case Style.SpikeExpo:
                return SpikeExpo;
            case Style.InQuad:
                return InQuad;
            case Style.OutQuad:
                return OutQuad;
            case Style.InOutQuad:
                return InOutQuad;
            case Style.SpikeQuad:
                return SpikeQuad;
            case Style.InCub:
                return InCub;
            case Style.OutCub:
                return OutCub;
            case Style.InOutCub:
                return InOutCub;
            case Style.SpikeCub:
                return SpikeCub;
            case Style.InQuart:
                return InQuart;
            case Style.OutQuart:
                return OutQuart;
            case Style.InOutQuart:
                return InOutQuart;
            case Style.SpikeQuart:
                return SpikeQuart;
            case Style.InQuint:
                return InQuint;
            case Style.OutQuint:
                return OutQuint;
            case Style.InOutQuint:
                return InOutQuint;
            case Style.SpikeQuint:
                return SpikeQuint;
            case Style.EaseInBounce:
                return EaseInBounce;
            case Style.EaseOutBounce:
                return EaseOutBounce;
            case Style.EaseInOutBounce:
                return EaseInOutBounce;
            case Style.SpikeBounce:
                return SpikeBounce;
            case Style.EaseInElastic:
                return EaseInElastic;
            case Style.EaseOutElastic:
                return EaseOutElastic;
            case Style.EaseInOutElastic:
                return EaseInOutElastic;
            case Style.SpikeElastic:
                return SpikeElastic;
            case Style.EaseInBack:
                return EaseInBack;
            case Style.EaseOutBack:
                return EaseOutBack;
            case Style.EaseInOutBack:
                return EaseInOutBack;
            case Style.SpikeBack:
                return SpikeBack;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    //converters
    public static float Ease(Function ease, float t)
    {
        return ease(t);
    }

    public static Vector2 Ease(Function ease, Vector2 a, Vector2 b, float t)
    {
        return new Vector2(ease(t), ease(t));
    }

    public static Vector3 Ease(Function ease, Vector3 a, Vector3 b, float t)
    {
        t = ease(t);
        return new Vector3(Interpolate(a.x, b.x, t), Interpolate(a.y, b.y, t), Interpolate(a.z, b.z, t));
    }

    //weighted average 
    //todo test this later if the calculation is actually correct
    public static float WeightedAverage(float a, float t, float slow)
    {
        t = (t * (slow - 1f) + a) / slow;
        return t;
    }

    //the easings
    public static float Interpolate(float a, float b, float t)
    {
        return a * (1 - t) + b * t;
    }

    public static float Linear(float t)
    {
        return t;
    }
    
    public static float SpikeLinear(float t)
    {
        t = t <= 0.5f ? 2 * t : 2 * (1 - t);
        return t;
    }

    public static float InSine(float t)
    {
        t = 1 - Mathf.Cos(t * Mathf.PI / 2);
        return t;
    }

    public static float OutSine(float t)
    {
        t = Mathf.Sin(t * Mathf.PI / 2);
        return t;
    }

    public static float InOutSine(float t)
    {
        t = (1 + Mathf.Sin(((t - 0.5f) * Mathf.PI))) / 2;
        return t;
    }

    public static float SpikeSine(float t)
    {
        t = t <= 0.5f ? 1 - Mathf.Cos(t * Mathf.PI) : Mathf.Cos(t * Mathf.PI) + 1;
        return t;
    }

    public static float InCircular(float t)
    {
        t = 1 - Mathf.Sqrt(1 - t * t);
        return t;
    }

    public static float OutCircular(float t)
    {
        t = Mathf.Sqrt(1 - (t - 1) * (t - 1));
        return t;
    }

    public static float InOutCircular(float t)
    {
        t = t < 0.5f ? 0.5f - Mathf.Sqrt(0.25f - t * t) : Mathf.Sqrt(0.25f - (t - 1) * (t - 1)) + 0.5f;
        return t;
    }

    public static float SpikeCircular(float t)
    {
        t = t < 0.5f ? 1 - Mathf.Sqrt(1 - 4 * Mathf.Pow(t, 2)) : 1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t - 2, 2));
        return t;
    }

    public static float InExpo(float t)
    {
        t = 1 - Mathf.Sqrt(1 - t);
        return t;
    }

    public static float OutExpo(float t)
    {
        t = Mathf.Sqrt(t);
        return t;
    }

    public static float InOutExpo(float t)
    {
        t = t <= 0.5f ? 0.5f - Mathf.Sqrt(1 - 2 * t) / 2 : Mathf.Sqrt(2 * t - 1) / 2 + 0.5f;
        return t;
    }

    public static float SpikeExpo(float t)
    {
        t = t <= 0.5f ? 1 - Mathf.Sqrt(1 - 2 * t) : 1 - Mathf.Sqrt(2 * t - 1);
        return t;
    }

    public static float InQuad(float t)
    {
        t = Mathf.Pow(t, 2);
        return t;
    }

    public static float OutQuad(float t)
    {
        t = 1 - Mathf.Pow(t - 1, 2);
        return t;
    }

    public static float InOutQuad(float t)
    {
        t = t <= 0.5f ? 2 * t * t : 1 - Mathf.Pow(2 * t - 2, 2) / 2;
        return t;
    }

    public static float SpikeQuad(float t)
    {
        t = t <= 0.5f ? 4 * Mathf.Pow(t, 2) : Mathf.Pow(2 * t - 2, 2);
        return t;
    }

    public static float InCub(float t)
    {
        t = Mathf.Pow(t, 3);
        return t;
    }

    public static float OutCub(float t)
    {
        t = Mathf.Pow(t - 1, 3) + 1;
        return t;
    }

    public static float InOutCub(float t)
    {
        t = t <= 0.5 ? 4 * Mathf.Pow(t, 3) : 4 * Mathf.Pow(t - 1, 3) + 1;
        return t;
    }

    public static float SpikeCub(float t)
    {
        t = t <= 0.5 ? 8 * Mathf.Pow(t, 3) : -Mathf.Pow(2 * t - 2, 3);
        return t;
    }

    public static float InQuart(float t)
    {
        t = Mathf.Pow(t, 4);
        return t;
    }

    public static float OutQuart(float t)
    {
        t = 1 - Mathf.Pow(t - 1, 4);
        return t;
    }

    public static float InOutQuart(float t)
    {
        t = t <= 0.5f ? -8 * Mathf.Pow(t - 0.5f, 4) + 0.5f : 8 * Mathf.Pow(t - 0.5f, 4) + 0.5f;
        return t;
    }

    public static float SpikeQuart(float t)
    {
        t = t <= 0.5 ? 16 * Mathf.Pow(t, 4) : Mathf.Pow(2 * t - 2, 4);
        return t;
    }

    public static float InQuint(float t)
    {
        t = Mathf.Pow(t, 5);
        return t;
    }

    public static float OutQuint(float t)
    {
        t = Mathf.Pow(t - 1, 5) + 1;
        return t;
    }

    public static float InOutQuint(float t)
    {
        t = t <= 0.5f ? 16 * Mathf.Pow(t, 5) : 16 * Mathf.Pow(t - 1, 5) + 1;
        return t;
    }

    public static float SpikeQuint(float t)
    {
        t = t <= 0.5 ? 32 * Mathf.Pow(t, 5) : -Mathf.Pow(2 * t - 2, 5);
        return t;
    }

    private static float BounceOut(float t)
    {
        float offset = 2.75f;
        float scalar = 7.5625f;

        if (t < 1 / offset)
        {
            t = scalar * Mathf.Pow(t, 2);
        }
        else if (t < 2 / offset)
        {
            t = scalar * Mathf.Pow(t - 1.5f / offset, 2) + 0.75f;
        }
        else if (t < 2.5f / offset)
        {
            t = scalar * Mathf.Pow(t - 2.25f / offset, 2) + 0.9375f;
        }
        else
        {
            t = scalar * Mathf.Pow(t - 2.625f / offset, 2) + 0.984375f;
        }
        return t;
    }

    public static float EaseInBounce(float t)
    {
        t = 1 - BounceOut(t);
        return t;
    }

    public static float EaseOutBounce(float t)
    {
        t = BounceOut(t);
        return t;
    }

    public static float EaseInOutBounce(float t)
    {
        t = t < 0.5f ? 1 - BounceOut(1 - 2 * t) / 2 - 0.5f : BounceOut(2 * t - 1) / 2 + 0.5f;
        return t;
    }
    
    public static float SpikeBounce(float t)
    {
        t = t < 0.5f ? 1 - BounceOut(1 - 2 * t) : 1 - BounceOut( 2 * t - 1);
        return t;
    }

    private static float ElasticIn(float t, float period,  float amplitude)
    {
        float tau = 2f * Mathf.PI;
        if (t == 0f)
        {
            return 0;
        }
        if (t == 1f)
        {
            return 1;
        }
        t = - Mathf.Pow(t * amplitude, 2) * Mathf.Sin((t - 0.75f) * tau * period);
        return t;
    }
    
    public static float EaseInElastic(float t)
    {
        float period = 3f;
        float amplitude = 1f;
        t = ElasticIn(t, period, amplitude);
        return t;
    }
    
    public static float EaseOutElastic(float t)
    {
        float period = 3f;
        float amplitude = 1f;
        t = 1 - ElasticIn(1 - t, period, amplitude);
        return t;
    }
    
    public static float EaseInOutElastic(float t)
    {
        float period = 3f * 1.65f;
        float amplitude = 1f * 2;
        t = t <= 0.5f ? ElasticIn(t, period, amplitude) / 2 : - ElasticIn((1 - t), period, amplitude) / 2 + 1;
        return t;
    }
    
    public static float SpikeElastic(float t)
    {
        float period = 3f * 1.65f;
        float amplitude = 1f * 2;
        t = t <= 0.5f ? 0.5f + ElasticIn(t, period, amplitude) / 2 : 0.5f + ElasticIn((1 - t), period, amplitude) / 2;
        return t;
    }
    
    public static float EaseInBack(float t)
    {
        return t;
    }
    
    public static float EaseOutBack(float t)
    {
        return t;
    }
    
    public static float EaseInOutBack(float t)
    {
        return t;
    }

    public static float SpikeBack(float t)
    {
        return t;
    }

}