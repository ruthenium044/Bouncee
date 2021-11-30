using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuickMath
{
    public static float SinPi(float x) //todo test it
    {
        float left = x % 2.0f;
        int multiplier = (int) ((x - left) / 2);
        x -= 2 * multiplier;
        
        x = 0.5f - x;
        float x2 = x * x;
        float x4 = x2 * x2;
        float x6 = x4 * x2;
      
        const float fa = 8.0f / 9.0f;
        const float fb = 34.0f / 9.0f;
        const float fc = 44.0f / 9.0f;
      
        x = - ( fa * x6 - fb * x4 + fc * x2 - 1 );
        return x;
    }
    
    public static float CosPi(float x)
    {
        x = 1 - x;
        float x2 = x * x;
        float x4 = x2 * x2;
        float x6 = x4 * x2;
      
        const float fa = 8.0f / 9.0f;
        const float fb = 34.0f / 9.0f;
        const float fc = 44.0f / 9.0f;
      
        x = fa * x6 - fb * x4 + fc * x2 - 1;
        return x;
    }
    
    //todo this wont work..
    private static unsafe uint AsInt(float f)
    {
        return *(uint*) &f; 
    }
    
    private static unsafe float AsFloat(uint i)
    {
        return *(float*) &i; 
    }

    public static float Pow(float x, float p)
    {
        return AsFloat((uint)(p * (AsInt(x) - 1)) + 1) ;
    }   
    
    public static unsafe float Qsqrt(float x)
    {
        float halfX = 0.5f * x;
        int i = *(int*) &x;
        i = 0x5f375a86 - (i >> 1);
        x = *(float*) &i;
        x = x * (1.5f - halfX * x * x);
        return x;
    }
}
