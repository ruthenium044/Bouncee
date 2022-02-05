using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBall : MonoBehaviour
{
    [SerializeField] private EasingUtility.Style style;
    [SerializeField] private EasingUtility.Mode mode;
    private EasingUtility.Function function;
    
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;

    [SerializeField] private float duration;

    public void SetValues(Vector3 aPos, Vector3 bPos, int style, int mode, float duration)
    {
        startPosition = aPos;
        endPosition = bPos;
        this.style = (EasingUtility.Style) style;
        this.mode = (EasingUtility.Mode) mode;
        this.duration = duration;
    }

    public IEnumerator Draw()
    {
        function = EasingUtility.GetFunction(style, mode);
        
        float t = 0;
        while (t < 1)
        {
            float x = Time.deltaTime / duration;
            float y = EasingUtility.Ease(function, startPosition.y, endPosition.y, t);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            
            t += x;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, function(1), transform.position.z);
        
        t = 1;
        while (t > 0)
        {
            float x = Time.deltaTime / duration;
            float y = EasingUtility.Ease(function, startPosition.y, endPosition.y, t);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            
            t -= x;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, function(1), transform.position.z);
        StartCoroutine(Draw());
    }
    

    private void Start()
    {
        StartCoroutine(Draw());
    }
}
