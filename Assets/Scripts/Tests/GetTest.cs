using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTest : MonoBehaviour
{
    [SerializeField] private int numberOfTests;
    [SerializeField] private int poisions;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PowTest();
        }
    }

    private void PowTest()
    {
        using (new CustomTimer("Mathf.Pow 2 ", numberOfTests))
        {
            for (int i = 0; i < numberOfTests; i++)
            {
                float x;
                for (int j = 0; j < poisions; j++)
                {
                    x = i / (float) poisions;
                    float y = Mathf.Pow(x, 2);
                }
            }
        }

        using (new CustomTimer("x 2 ", numberOfTests))
        {
            for (int i = 0; i < numberOfTests; i++)
            {
                float x;
                for (int j = 0; j < poisions; j++)
                {
                    x = i / (float) poisions;
                    float y = x * x;
                }
            }
        }
        
        using (new CustomTimer("Mathf.Pow 5 ", numberOfTests))
        {
            for (int i = 0; i < numberOfTests; i++)
            {
                float x;
                for (int j = 0; j < poisions; j++)
                {
                    x = i / (float) poisions;
                    float y = Mathf.Pow(x, 5);
                }
            }
        }

        using (new CustomTimer("x 5 ", numberOfTests))
        {
            for (int i = 0; i < numberOfTests; i++)
            {
                float x;
                for (int j = 0; j < poisions; j++)
                {
                    x = i / (float) poisions;
                    float y = x * x * x * x * x;
                }
            }
        }
    }
    
    private void CosTest()
    {
        using (new CustomTimer("Mathf.Cos ", numberOfTests))
        {
            for (int i = 0; i < numberOfTests; i++)
            {
                float x;
                for (int j = 0; j < poisions; j++)
                {
                    x = i / (float) poisions;
                    float y = 1 - Mathf.Cos(0.5f * x * Mathf.PI);
                }
            }
        }

        using (new CustomTimer("QuickMath.Cos ", numberOfTests))
        {
            for (int i = 0; i < numberOfTests; i++)
            {
                float x;
                for (int j = 0; j < poisions; j++)
                {
                    x = i / (float) poisions;
                    float y = 1 - QuickMath.CosPi(0.5f * x);
                }
            }
        }
    }
}
