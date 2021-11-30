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
}
