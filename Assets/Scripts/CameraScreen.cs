using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreen : MonoBehaviour
{
    [SerializeField] private Transform spwaner;
    [SerializeField] private Vector2 offset;
    private Vector3 camPos;
    
    [SerializeField] private float timeBeforeSwitch = 2;
    [SerializeField] private float timeToSwitch = 2;
    
    
    [SerializeField] private bool StripCam = false;
    
    void Start()
    {
        camPos = transform.position;
        if (StripCam)
        {
            StartCoroutine(RunStrips());
        }
        else
        {
            StartCoroutine(RunAll());
        }
    }

    IEnumerator RunAll()
    {
        yield return new WaitForSeconds(2f);
        float i = 0;
        foreach(Transform child in spwaner)
        {
            transform.position = new Vector3(child.position.x + offset.x, child.position.y + offset.y, transform.position.z);
            ScreenCapture.CaptureScreenshot(Application.dataPath  + "/Screenshots/" + i + "ease.png", 5);
            UnityEditor.AssetDatabase.Refresh();
            i++;
            Debug.Log("Here");
            yield return new WaitForSeconds(2f);
        }
    }
    
    IEnumerator RunStrips()
    {
        yield return new WaitForSeconds(timeBeforeSwitch);
        float i = 0;
        foreach(Transform child in spwaner)
        {
            transform.position = new Vector3(camPos.x, child.position.y + offset.y, transform.position.z);
            Debug.Log("Here");
            yield return new WaitForSeconds(timeToSwitch);
            i++;
        }
    }

}
