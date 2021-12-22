using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

[CustomPropertyDrawer(typeof(UnityEasingTimer))]
public class UnityEasingTimerPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        float labelWidth = 100;
        float fieldWidth = 40;
        
        GUILayout.Label("Easing", EditorStyles.boldLabel,GUILayout.Width(labelWidth));
        
        GUILayout.BeginVertical(EditorStyles.helpBox);
        EasingField(property, labelWidth);
        TimeField(property, labelWidth, fieldWidth);
        DurationField(property, labelWidth, fieldWidth);
        GUILayout.EndVertical();
        
        GUILayout.Label("Animation", EditorStyles.boldLabel,GUILayout.Width(labelWidth));
        
        GUILayout.BeginVertical(EditorStyles.helpBox);
        AnimationField(property, labelWidth);
        GUILayout.EndVertical();

        //todo what to do here later
        //State state = State.Stopping; 
        //UnityEvent onStartUnityEvent;
        //UnityEvent<EaseData> onGoingUnityEvent;
        //UnityEvent onEndUnityEvent;
        //List<TimedEvent> timePointUnityEvents = new List<TimedEvent>();
    }
    
    static void HorizontalLine ( Color color ) {
        
        EditorGUILayout.Space(10.0f);
        
        GUIStyle horizontalLine = new GUIStyle
        {
            normal =
            {
                background = EditorGUIUtility.whiteTexture
            },
            margin = new RectOffset( 0, 0, 4, 4 ),
            fixedHeight = 1
        };

        var c = GUI.color;
        GUI.color = color;
        GUILayout.Box( GUIContent.none, horizontalLine );
        GUI.color = c;
    }
    
    private static void AnimationField(SerializedProperty property, float width)
    {
        var reverseProperty = property.FindPropertyRelative("isReverse");
        int loopProperty;
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Loop style", GUILayout.Width(width));
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        reverseProperty.boolValue = EditorGUILayout.ToggleLeft("Reverse", reverseProperty.boolValue, GUILayout.Width(width));
        GUILayout.EndHorizontal();
    }

    private static bool showDuration;
    private static bool showSpeed;
    
    private static void DurationField(SerializedProperty property, float width, float width2)
    {
        var durationProperty = property.FindPropertyRelative("duration");
        var speedProperty = property.FindPropertyRelative("speed");
        
        GUILayout.BeginHorizontal();
        showDuration = EditorGUILayout.ToggleLeft("Duration", showDuration, GUILayout.Width(width));
        EditorGUI.BeginDisabledGroup(!showDuration);
        durationProperty.doubleValue = EditorGUILayout.DoubleField(durationProperty.doubleValue, GUILayout.Width(width2));
        EditorGUI.EndDisabledGroup();
        
        showSpeed = EditorGUILayout.ToggleLeft("Speed", showSpeed, GUILayout.Width(width));
        EditorGUI.BeginDisabledGroup(!showSpeed);
        speedProperty.doubleValue = EditorGUILayout.DoubleField(speedProperty.doubleValue, GUILayout.Width(width2));
        EditorGUI.EndDisabledGroup();
        
        GUILayout.EndHorizontal();
    }

    private static void TimeField(SerializedProperty property, float width, float width2)
    {
        var timeModeProperty = property.FindPropertyRelative("timeMode");
        var customDeltaTimeProperty = property.FindPropertyRelative("customDeltaTime");
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Time mode", GUILayout.Width(width));

        TimeMode timeMode = (TimeMode) timeModeProperty.enumValueIndex;
        timeModeProperty.enumValueIndex = (int) (TimeMode) EditorGUILayout.EnumPopup(timeMode);
        GUILayout.EndHorizontal();

        if (timeMode is TimeMode.UnscaledWaitForSeconds or TimeMode.WaitForSeconds)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Custom time", GUILayout.Width(width));
            customDeltaTimeProperty.doubleValue = EditorGUILayout.DoubleField(customDeltaTimeProperty.doubleValue, GUILayout.Width(width2));
            GUILayout.EndHorizontal();
        }
    }

    private static void EasingField(SerializedProperty property, float width)
    {
        var easingStyleProperty = property.FindPropertyRelative("easingStyle");
        var easingModeProperty = property.FindPropertyRelative("easingMode");
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Easing", GUILayout.Width(width));
        EasingUtility.Style style = (EasingUtility.Style) easingStyleProperty.enumValueIndex;
        easingStyleProperty.enumValueIndex = (int) (EasingUtility.Style) EditorGUILayout.EnumPopup(style);

        EasingUtility.Mode mode = (EasingUtility.Mode) easingModeProperty.enumValueIndex;
        easingModeProperty.enumValueIndex = (int) (EasingUtility.Mode) EditorGUILayout.EnumPopup(mode);
        GUILayout.EndHorizontal();
    }
}
