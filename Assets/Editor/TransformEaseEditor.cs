using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[UnityEditor.CustomEditor(typeof(TransformEase))]
public class TransformEaseEditor : UnityEditor.Editor
{

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        UnityEditor.EditorGUILayout.Space(20f);

        if (GUILayout.Button("Save current Transform as Start")) {
            var target = serializedObject.targetObject as TransformEase;
            target.SetStartPosition(target.transform.position);
            target.SetStartRotation(target.transform.rotation.eulerAngles);
            target.SetStartScale(target.transform.localScale);
        }

        if (GUILayout.Button("Save current Transform as End")) {
            var target = serializedObject.targetObject as TransformEase;
            target.SetEndPosition(target.transform.position);
            target.SetEndRotation(target.transform.rotation.eulerAngles);
            target.SetEndScale(target.transform.localScale);
        }



    }

}

