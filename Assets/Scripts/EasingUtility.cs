using System;
using UnityEngine;

public static class EasingUtility
{
    public delegate float Function(float t);

    #region Styles
    
    public enum Style
    {
        Linear,
        SpikeLinear,
        InSine,
        OutSine,
        InOutSine,
        SpikeSine,
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
        InExpo,
        OutExpo,
        InOutExpo,
        SpikeExpo,
        InCircular,
        OutCircular,
        InOutCircular,
        SpikeCircular,
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
            case Style.InExpo:
                return InExpo;
            case Style.OutExpo:
                return OutExpo;
            case Style.InOutExpo:
                return InOutExpo;
            case Style.SpikeExpo:
                return SpikeExpo;
            case Style.InCircular:
                return InCircular;
            case Style.OutCircular:
                return OutCircular;
            case Style.InOutCircular:
                return InOutCircular;
            case Style.SpikeCircular:
                return SpikeCircular;
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
                return EaseInBackSmall;
            case Style.EaseOutBack:
                return EaseOutBackSmall;
            case Style.EaseInOutBack:
                return EaseInOutBackSmall;
            case Style.SpikeBack:
                return SpikeBackSmall;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    #endregion
    
    //todo David get these to your stuff from here ples
    //converters
    public static float Interpolate(float a, float b, float t)
    { //maybe add exterpolate later
        return a * (1 - t) + b * t;
    }
    
    public static float Ease(Function ease, float a, float b, float t)
    {
        t = ease(t);
        return Interpolate(a, b, t);
    }

    public static Vector2 Ease(Function ease, Vector2 a, Vector2 b, float t)
    {
        t = ease(t);
        return new Vector2(Interpolate(a.x, b.x, t), Interpolate(a.y, b.y, t));
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
    
    //Helper functions
    private static float tau = 2f * Mathf.PI;
    
    private static float Invert(float t)
    {
        return 1 - t;
    }

    #region Easing 
    //the easings
  
    
    #region Linear
    public static float Linear(float t)
    {
        return t;
    }
    
    public static float SpikeLinear(float t)
    {
        t = t <= 0.5f ? 2 * Linear(t) : 2 * Linear((1 - t));
        return t;
    }
    #endregion
    
    #region Sine
    public static float InSine(float t)
    {
        t = 1 - Mathf.Cos(t * Mathf.PI / 2);
        return t;
    }

    public static float OutSine(float t)
    {
        t = Invert(InSine(Invert(t)));
        return t;
    }

    public static float InOutSine(float t)
    {
        t = InSine(2 * t) / 2;
        return t;
    }

    public static float SpikeSine(float t)
    {
        t = t <= 0.5f ? InSine(2 * t) : 2 - InSine(2 * t);
        return t;
    }
    #endregion

    #region Quad
    public static float InQuad(float t)
    {
        t = t * t;
        return t;
    }

    public static float OutQuad(float t)
    {
        t = Invert(InQuad(Invert(t)));
        return t;
    }

    public static float InOutQuad(float t)
    {
        t = t <= 0.5f ? 2 * InQuad(t) : 2 * OutQuad(t) - 1;
        return t;
    }

    public static float SpikeQuad(float t)
    {
        t = t <= 0.5f ? 4 * InQuad(t) : 4 * InQuad(t - 1);
        return t;
    }
    #endregion

    #region Cubic
    public static float InCub(float t)
    {
        t = t * t * t;
        return t;
    }

    public static float OutCub(float t)
    {
        t = Invert(InCub(Invert(t)));
        return t;
    }

    public static float InOutCub(float t)
    {
        t = t <= 0.5 ? 4 * InCub(t) : 4 * OutCub(t) - 3;
        return t;
    }

    public static float SpikeCub(float t)
    {
        t = t <= 0.5f ? 8 * InCub(t) : - 8 * InCub(t - 1);
        return t;
    }
    #endregion

    #region Quart
    public static float InQuart(float t)
    {
        t = t * t * t * t;
        return t;
    }

    public static float OutQuart(float t)
    {
        t = Invert(InQuart(Invert(t)));
        return t;
    }

    public static float InOutQuart(float t)
    {
        t = t <= 0.5f ? 8 * InQuart(t) : 8 * OutQuart(t) - 7;
        return t;
    }

    public static float SpikeQuart(float t)
    {
        t = t <= 0.5f ? 16 * InQuart(t) : 16 * InQuart(t - 1);
        return t;
    }
    #endregion
    
    #region Quint
    public static float InQuint(float t)
    {
        t = t * t * t * t * t;
        return t;
    }

    public static float OutQuint(float t)
    {
        t = Invert(InQuint(Invert(t)));
        return t;
    }

    public static float InOutQuint(float t)
    {
        t = t <= 0.5f ? 16 * InQuint(t) : 16 * OutQuint(t) - 15;
        return t;
    }

    public static float SpikeQuint(float t)
    {
        t = t <= 0.5f ? 32 * InQuint(t) : - 32 * InQuint(t - 1);
        return t;
    }
    #endregion
    
    #region Expo
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
    #endregion
    
    #region Circular
    
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
    #endregion

    #region Bounce
    public static float EaseInBounce(float t)
    {
        t = Invert(EaseOutBounce(Invert(t)));
        return t;
    }

    public static float EaseOutBounce(float t)
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

    public static float EaseInOutBounce(float t)
    {
        t = t < 0.5f ? Invert(EaseOutBounce(1 - 2 * t)) / 2 : EaseOutBounce(2 * t - 1) / 2 + 0.5f;
        return t;
    }
    
    public static float SpikeBounce(float t)
    {
       t = t < 0.5f ? Invert(EaseOutBounce(1 - 2 * t)) : Invert(EaseOutBounce( 2 * t - 1));
        return t;
    }
    #endregion

    #region Elast
    //todo get rid of all pows
    public static float EaseInElastic(float t, float amplitude, float period)
    {
        if (t <= 0f)
        {
            return 0;
        }
        if (t >= 1f)
        {
            return 1;
        }
        t = - Mathf.Pow(t * amplitude, 2) * Mathf.Sin((t - 0.75f) * tau * period);
        return t;
    }
    
    public static float EaseInElastic(float t)
    {
        t = EaseInElastic(t, 1f, 3f);
        return t;
    }
    
    public static float EaseOutElastic(float t)
    {
        t = Invert(EaseInElastic(Invert(t)));
        return t;
    }
    
    public static float EaseInOutElastic(float t)
    {
        float amplitude = 1f * 2f;
        float period = 3f * 1.645f;
        t = t <= 0.5f ? EaseInElastic(t, amplitude, period) / 2 : Invert(EaseInElastic(Invert(t), amplitude, period)) / 2 + 0.5f;
        return t;
    }

    public static float SpikeElastic(float t)
    {
        float amplitude = 1f * 2f;
        float period = 3f * 1.645f;
        t = t <= 0.5f ? EaseInElastic(t, amplitude, period) :  EaseInElastic(Invert(t), amplitude, period);
        return t;
    }
    #endregion
    
    #region Back Small
    public static float EaseInBackSmall(float t)
    {
        float a = 1.70158f;
        t = (a + 1) * InCub(t) - a * t * t;
        return t;
    }
    
    public static float EaseOutBackSmall(float t)
    {
        t = Invert(EaseInBackSmall(Invert(t)));
        return t;
    }
    
    public static float EaseInOutBackSmall(float t)
    {
        float p = 1.75f;
        t = t <= 0.5f ? EaseInBackSmall(p * t) : Invert(EaseInBackSmall(p * Invert(t)));
        return t;
    }

    public static float SpikeBackSmall(float t)
    {
        float p = 2f;
        t = t <= 0.5f ? EaseInBackSmall(p * t) : EaseInBackSmall(p * Invert(t));
        return t;
    }
    #endregion
    
    
    #region Back Big
    public static float EaseInBackBig(float t)
    {
        return t;
    }
    
    public static float EaseOutBackBig(float t)
    {
        return t;
    }
    
    public static float EaseInOutBackBig(float t)
    {
        return t;
    }

    public static float SpikeBackBig(float t)
    {
        return t;
    }
    #endregion

    #endregion \\Easing
}