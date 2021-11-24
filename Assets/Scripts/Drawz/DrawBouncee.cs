using System;
using System.Collections;
using System.Collections.Generic;
using Drawz;
using UnityEngine;

public class DrawBouncee : DrawShapes
{
   
    void Start()
    {
        Initialize();
    }

    public override void Draw()
    {
        lineRenderer.positionCount = vertexCount;
        float x = 0f;
        float y;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += 0.01f;
            y = Mathf.Sin(x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + transform.position);
        }
    }

    private void Update()
    {
        Draw();
    }
}
