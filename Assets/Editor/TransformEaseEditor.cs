using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TransformEase))]

public class TransformEaseEditor : Editor
{
    private bool isInStartEdit = false;
    private bool isInEndEdit = false;
    private GameObject phantom = null;

    private const string StartEditOn =  "Edit Start Transform: On";
    private const string StartEditOff = "Edit Start Transform: Off";

    private const string EndEditOn =  "Edit End Transform: On";
    private const string EndEditOff = "Edit End Transform: Off";

	private void OnEnable()
	{
        isInEndEdit = false;
    }

	private void OnDisable()
	{
        if(phantom != null) {
            DestroyImmediate(phantom);
        }
        isInEndEdit = false;
    }

	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.Space(20f);

        var target = serializedObject.targetObject as TransformEase;

        if(!isInEndEdit) {
            if (GUILayout.Button(isInStartEdit ? StartEditOn : StartEditOff)) {
                ToggleEditMode(target, ref isInStartEdit, (TransformEase that) =>
                {
                    that.OnStart();
                });
            }
		}

        if(!isInStartEdit) {
            if(GUILayout.Button(isInEndEdit ? EndEditOn : EndEditOff)) {
                ToggleEditMode(target, ref isInEndEdit, (TransformEase that) =>
                {
                    that.OnEnd();
                });
            }
		}


        ActiveEditorTracker.sharedTracker.isLocked = isInEndEdit || isInStartEdit;
        if (isInStartEdit) {
            InStartEditMode(target);
        }
        else if (isInEndEdit) {
            IsEndEditMode(target);
		}
    }

    private void InStartEditMode(TransformEase target)
    {
        target.SetStartPosition(phantom.transform.position);
        target.SetStartRotation(phantom.transform.rotation.eulerAngles);
        target.SetStartScale(phantom.transform.localScale);
    }

    private void IsEndEditMode(TransformEase target)
    {
        target.SetEndPosition(phantom.transform.position);
        target.SetEndRotation(phantom.transform.rotation.eulerAngles);
        target.SetEndScale(phantom.transform.localScale);
    }

    private void ToggleEditMode(TransformEase target, ref bool toToggle, System.Action<TransformEase> transformSetter)
    {
        toToggle = !toToggle;

        if (toToggle) {
            phantom = Instantiate(target.gameObject);
            transformSetter(phantom.GetComponent<TransformEase>());

            ActiveEditorTracker.sharedTracker.isLocked = true;
            Selection.activeTransform = phantom.transform;
            DestroyImmediate(phantom.GetComponent<TransformEase>());
        }
        else {
            DestroyImmediate(phantom);
            ActiveEditorTracker.sharedTracker.isLocked = false;
            Selection.activeTransform = target.transform;
        }
    }
}

