using System;
using UnityEngine;

public static class EasingUtility
{
    public delegate float Function(float t);

    #region Styles
    
    public enum Mode
    {
        In,
        Out,
        InOut,
        Spike
    }

    public static int ModeCount = (int) Mode.Spike + 1;

    public enum Style
    {
        Linear,
        Sine,
        Quadratic,
        Cubic,
        Quartic,
        Quintic,
        Exponential,
        Circular,
        Bounce,
        Elastic,
        Back
    }
    
    public static int StyleCount = (int) Style.Back + 1;
    
    public static Function GetFunction(Style style, Mode mode)
    {
        switch (mode)
        {
            case Mode.In:
                return GetIn(style);
            case Mode.Out:
                return GetOut(style);
            case Mode.InOut:
                return GetInOut(style);
            case Mode.Spike:
                return GetSpike(style);
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    private static Function GetIn(Style style)
    {
        switch (style)
        {
            case Style.Linear:
                return Linear;
            case Style.Sine:
                return InSine;
            case Style.Quadratic:
                return InQuad;
            case Style.Cubic:
                return InCub;
            case Style.Quartic:
                return InQuart;
            case Style.Quintic:
                return InQuint;
            case Style.Exponential:
                return InExpo;
            case Style.Circular:
                return InCircular;
            case Style.Bounce:
                return InBounce;
            case Style.Elastic:
                return InElastic;
            case Style.Back:
                return InBack;
            default:
                throw new ArgumentOutOfRangeException(nameof(style), style, null);
        }
    }
    
    private static Function GetOut(Style style)
    {
        switch (style)
        {
            case Style.Linear:
                return Linear;
            case Style.Sine:
                return OutSine;
            case Style.Quadratic:
                return OutQuad;
            case Style.Cubic:
                return OutCub;
            case Style.Quartic:
                return OutQuart;
            case Style.Quintic:
                return OutQuint;
            case Style.Exponential:
                return OutExpo;
            case Style.Circular:
                return OutCircular;
            case Style.Bounce:
                return OutBounce;
            case Style.Elastic:
                return OutElastic;
            case Style.Back:
                return OutBack;
            default:
                throw new ArgumentOutOfRangeException(nameof(style), style, null);
        }
    }
    
    private static Function GetInOut(Style style)
    {
        switch (style)
        {
            case Style.Linear:
                return Linear;
            case Style.Sine:
                return InOutSine;
            case Style.Quadratic:
                return InOutQuad;
            case Style.Cubic:
                return InOutCub;
            case Style.Quartic:
                return InOutQuart;
            case Style.Quintic:
                return InOutQuint;
            case Style.Exponential:
                return InOutExpo;
            case Style.Circular:
                return InOutCircular;
            case Style.Bounce:
                return InOutBounce;
            case Style.Elastic:
                return InOutElastic;
            case Style.Back:
                return InOutBack;
            default:
                throw new ArgumentOutOfRangeException(nameof(style), style, null);
        }
    }
    
    private static Function GetSpike(Style style)
    {
        switch (style)
        {
            case Style.Linear:
                return SpikeLinear;
            case Style.Sine:
                return SpikeSine;
            case Style.Quadratic:
                return SpikeQuad;
            case Style.Cubic:
                return SpikeCub;
            case Style.Quartic:
                return SpikeQuart;
            case Style.Quintic:
                return SpikeQuint;
            case Style.Exponential:
                return SpikeExpo;
            case Style.Circular:
                return SpikeCircular;
            case Style.Bounce:
                return SpikeBounce;
            case Style.Elastic:
                return SpikeElastic;
            case Style.Back:
                return SpikeBack;
            default:
                throw new ArgumentOutOfRangeException(nameof(style), style, null);
        }
    }
    
    #endregion
    
    //todo David get these to your stuff from here ples
    //converters
    public static float Interpolate(float a, float b, float t)
    { //todo maybe add exterpolate later
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
        t = 1 - QuickMath.CosPi(t * 0.5f);
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
        t = t < 0.5f ? 1 - Mathf.Sqrt(1 - 4 * t * t) : 1 - Mathf.Sqrt(1 - (2 * t - 2) * (2 * t - 2));
        return t;
    }
    #endregion

    #region Bounce
    public static float InBounce(float t)
    {
        t = Invert(OutBounce(Invert(t)));
        return t;
    }

    public static float OutBounce(float t)
    {
        float offset = 2.75f;
        float scalar = 7.5625f;

        if (t < 1 / offset)
        {
            t = scalar * t * t;
        }
        else if (t < 2 / offset)
        {
            t = scalar * InQuad(t - 1.5f / offset) + 0.75f;
        }
        else if (t < 2.5f / offset)
        {
            t = scalar * InQuad(t - 2.25f / offset) + 0.9375f;
        }
        else
        {
            t = scalar * InQuad(t - 2.625f / offset) + 0.984375f;
        }
        return t;
    }

    public static float InOutBounce(float t)
    {
        t = t < 0.5f ? Invert(OutBounce(1 - 2 * t)) / 2 : OutBounce(2 * t - 1) / 2 + 0.5f;
        return t;
    }
    
    public static float SpikeBounce(float t)
    {
       t = t < 0.5f ? Invert(OutBounce(1 - 2 * t)) : Invert(OutBounce( 2 * t - 1));
        return t;
    }
    #endregion

    #region Elast
    public static float InElastic(float t, float amplitude, float period)
    {
        if (t <= 0f)
        {
            return 0;
        }
        if (t >= 1f)
        {
            return 1;
        }
        t = - InQuad(t * amplitude) * Mathf.Sin((t - 0.75f) * Mathf.PI * 2 * period);
        return t;
    }
    
    public static float InElastic(float t)
    {
        t = InElastic(t, 1f, 3f);
        return t;
    }
    
    public static float OutElastic(float t)
    {
        t = Invert(InElastic(Invert(t)));
        return t;
    }
    
    public static float InOutElastic(float t)
    {
        float amplitude = 1f * 2f;
        float period = 3f * 1.645f;
        t = t <= 0.5f ? InElastic(t, amplitude, period) / 2 : Invert(InElastic(Invert(t), amplitude, period)) / 2 + 0.5f;
        return t;
    }

    public static float SpikeElastic(float t)
    {
        float amplitude = 1f * 2f;
        float period = 3f * 1.645f;
        t = t <= 0.5f ? InElastic(t, amplitude, period) :  InElastic(Invert(t), amplitude, period);
        return t;
    }
    #endregion
    
    #region Back
    public static float InBack(float t)
    {
        float a = 1.70158f;
        t = (a + 1) * InCub(t) - a * t * t;
        return t;
    }
    
    public static float OutBack(float t)
    {
        t = Invert(InBack(Invert(t)));
        return t;
    }
    
    public static float InOutBack(float t)
    {
        float p = 1.75f;
        t = t <= 0.5f ? InBack(p * t) : Invert(InBack(p * Invert(t)));
        return t;
    }

    public static float SpikeBack(float t)
    {
        float p = 2f;
        t = t <= 0.5f ? InBack(p * t) : InBack(p * Invert(t));
        return t;
    }
    #endregion

    #endregion \\Easing
    
    //Helper functions

    private static float Invert(float t)
    {
        return 1 - t;
    }
    
    //weighted average 
    //todo test this later if the calculation is actually correct
    public static float WeightedAverage(float a, float t, float slow)
    {
        t = (t * (slow - 1f) + a) / slow;
        return t;
    }

}