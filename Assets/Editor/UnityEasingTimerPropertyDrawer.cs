using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(UnityEasingTimer))]
public class UnityEasingTimerPropertyDrawer : PropertyDrawer
{
    private bool toggleDuration;
    private bool toggleSpeed;
    private static bool toggleLoop;
    
    private static readonly List<Vector3> points = new List<Vector3>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        float labelWidth = 100;
        float fieldWidth = 40;
        float graphWidth = 300;
        
        //Easing
        GUILayout.Label("Easing", EditorStyles.boldLabel,GUILayout.Width(labelWidth));
        EasingSection(property, labelWidth, fieldWidth);
        
        //Animation
        GUILayout.Label("Animation", EditorStyles.boldLabel,GUILayout.Width(labelWidth));
        AnimationSection(property, labelWidth);

        //Line
        GraphSection(graphWidth);
        
        //todo what to do here later
        //State state = State.Stopping; 
        //UnityEvent onStartUnityEvent;
        //UnityEvent<EaseData> onGoingUnityEvent;
        //UnityEvent onEndUnityEvent;
        //List<TimedEvent> timePointUnityEvents = new List<TimedEvent>();
    }

    private static void GraphSection(float width)
    {
        GUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(width));
        DrawLine(Color.green, width);
        GUILayout.EndVertical();
    }

    private void EasingSection(SerializedProperty property, float labelWidth, float fieldWidth)
    {
        GUILayout.BeginVertical(EditorStyles.helpBox);
        EasingField(property, labelWidth);
        TimeField(property, labelWidth, fieldWidth);
        (toggleDuration, toggleSpeed) = SpeedDurationField(property, labelWidth, fieldWidth, toggleDuration, toggleSpeed);
        GUILayout.EndVertical();
    }
    
    private static void AnimationSection(SerializedProperty property, float width)
    {
        var reverseProperty = property.FindPropertyRelative("isReverse");
        var loopProperty = property.FindPropertyRelative("loop");
        
        GUILayout.BeginVertical(EditorStyles.helpBox);
        
        GUILayout.BeginHorizontal();
        toggleLoop = EditorGUILayout.ToggleLeft("Looping", toggleLoop, GUILayout.Width(width));
        reverseProperty.boolValue = EditorGUILayout.ToggleLeft("Reverse", reverseProperty.boolValue, GUILayout.Width(width));
        GUILayout.EndHorizontal();
        
        if (toggleLoop)
        {
            GUILayout.Label("Loop style", GUILayout.Width(width));
            //EasingUtility.Style style = (EasingUtility.Style) loopProperty.enumValueIndex;
            //loopProperty.enumValueIndex = (int) (EasingUtility.Style) EditorGUILayout.EnumPopup(style);
        }
        GUILayout.EndVertical();   
    }
    
    private static (bool showDuration, bool showSpeed) SpeedDurationField(SerializedProperty property, float width, float width2, bool showDuration, bool showSpeed)
    {
        var durationProperty = property.FindPropertyRelative("duration");
        var speedProperty = property.FindPropertyRelative("speed");
        
        GUILayout.BeginHorizontal();
        
        showDuration = DrawToggleField("Duration", width, width2, showDuration, durationProperty);
        showSpeed = DrawToggleField("Speed", width, width2, showSpeed, speedProperty);

        GUILayout.EndHorizontal();
        return (showDuration, showSpeed);
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
    
    private static bool DrawToggleField(string label, float width, float width2, bool showBool, SerializedProperty property)
    {
        showBool = EditorGUILayout.ToggleLeft(label, showBool, GUILayout.Width(width));
        EditorGUI.BeginDisabledGroup(!showBool);
        property.doubleValue = EditorGUILayout.DoubleField(property.doubleValue, GUILayout.Width(width2));
        EditorGUI.EndDisabledGroup();
        return showBool;
    }

    static void DrawLine (Color color, float width)
    {
        int times = 100;
        for (int i = 0; i < times; i++)
        {
            float t = i / (float) times;
            float x = t;
            
            //todo David how do i get the value here pls!!!!
            float y = 1 - EasingUtility.InBounce(x);
            
            points.Add(new Vector3(x * width, y * width * 0.25f + width * 0.2f, 0));
        }
 
        Rect rect = GUILayoutUtility.GetRect(10, width * 0.5f, 
                                            width * 0.25f, width * 0.65f);
        if (Event.current.type == EventType.Repaint)
        {
            GUI.BeginClip(rect);
            Handles.color = color;
            Handles.DrawLines(points.ToArray());
            GUI.EndClip();
        }
    }
}
