using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreen : MonoBehaviour
{
    [SerializeField] private Transform spwaner;
    [SerializeField] private Vector2 offset;
    
    void Start()
    {
        StartCoroutine(RunAll());
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
            yield return new WaitForSeconds(0.2f);
        }
    }

}
