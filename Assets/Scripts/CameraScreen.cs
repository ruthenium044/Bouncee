using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreen : MonoBehaviour
{
    [SerializeField] private Transform spwaner;
    
    void Start()
    {
        StartCoroutine(RunAll());
    }

    IEnumerator RunAll()
    {
        float i = 0;
        foreach(Transform child in spwaner)
        {
            transform.position = new Vector3(child.position.x + 0.5f, child.position.y + 0.125f, transform.position.z);
            ScreenCapture.CaptureScreenshot(Application.dataPath  + "/Screenshots/" + i + "ease.png", 5);
            UnityEditor.AssetDatabase.Refresh();
            i++;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
