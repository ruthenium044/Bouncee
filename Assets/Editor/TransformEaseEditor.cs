using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

[CustomEditor(typeof(TransformEase))]

public class TransformEaseEditor : Editor
{
    private readonly IEnumerable<System.Type> TypeFilter = new System.Type[] { typeof(float), 
                                                                               typeof(Vector2), 
                                                                               typeof(Vector3), 
                                                                               typeof(Color)};

    private const BindingFlags MemberFlags = BindingFlags.Instance |
                                             BindingFlags.Public |
                                             BindingFlags.NonPublic | 
                                             BindingFlags.GetProperty |
                                             BindingFlags.SetProperty;

    GameObject gameObject = null;
    TransformEase ease = null;
    private class TypeBreakDown
    {
        private object context = null;
        private List<FieldInfo> fields = new List<FieldInfo>();
        private List<PropertyInfo> properties = new List<PropertyInfo>();
        private List<TypeBreakDown> children = new List<TypeBreakDown>();

        private TypeBreakDown()
        {

		}

        public static List<TypeBreakDown> Create(params object[] objects)
        {
            List<TypeBreakDown> list = new List<TypeBreakDown>();
            foreach(var obj in objects) {
                list.Add(new TypeBreakDown(obj));
            }
            return list;
        }

        private static Stack<TypeBreakDown> Create(TypeBreakDown current, object context)
        {
            Stack<TypeBreakDown> stack = new Stack<TypeBreakDown>();
            current.context = context;
            var type = current.context.GetType();
            while (type != null) {
                var fields = type.GetFields(MemberFlags);
                var properties = type.GetProperties(MemberFlags);
    
                current.fields.AddRange(fields.Where(item => (item.FieldType.IsValueType &&
                                                              !item.FieldType.IsPointer)));
                current.properties.AddRange(properties.Where(item => (item.PropertyType.IsValueType &&
                                                                      !item.PropertyType.IsPointer &&
                                                                      item.CanRead &&
                                                                      item.CanWrite)));
                foreach (var field in current.fields) {
                    var next = new TypeBreakDown();
                    next.context = field.FieldType.IsValueType ? context : field.GetValue(current.context);
                    stack.Push(next);
                }
                foreach (var property in current.properties) {
                    var next = new TypeBreakDown();
                    next.context = property.PropertyType.IsValueType ? context : property.GetValue(current.context);
                    stack.Push(next);
                }
                type = type.BaseType;
                break;
            }

            return stack;
        }

        public TypeBreakDown(object obj)
        {
            context = obj;

            var stack = Create(this, obj);
			while (stack.Count > 0) {
				children.Add(stack.Pop());
			}
		}

        public delegate void OnAction(TypeBreakDown leaf, List<TypeBreakDown> branch);
        public void ForEachBranch(OnAction action)
        {
            foreach (var child in children) {
                var branch = new List<TypeBreakDown>{ this };
                ForEachBranchRecurse(action, child, branch);
            }
		}

        private static void ForEachBranchRecurse(OnAction action, TypeBreakDown current, List<TypeBreakDown> branch) {
            if(current.children.Count == 0) {
                action(current, branch);
			} 
            else {
                foreach (var child in current.children) {
                    branch.Add(current);
                    ForEachBranchRecurse(action, child, branch);

                }
            }
		}
    }

    //private HashSet<(object context, object variable)> selectedData = new HashSet<(object context, object variable)>();

    HashSet<Component> components = new HashSet<Component>();
    Dictionary<Component, List<FieldInfo>> componentFields = new Dictionary<Component, List<FieldInfo>>();
    Dictionary<Component, List<PropertyInfo>> componentProperties = new Dictionary<Component, List<PropertyInfo>>();
    GenericMenu menu = new GenericMenu();

    private void OnEnable()
	{
        ease = serializedObject.targetObject as TransformEase;

        gameObject = ease.gameObject;

        var allComponents = gameObject.GetComponents<Component>();
		componentFields.Clear();
		componentProperties.Clear();
		components.Clear();

		foreach (var component in allComponents) {
			var componentType = component.GetType();
			if (!componentFields.ContainsKey(component)) {
				componentFields[component] = new List<FieldInfo>();
			}
			if (!componentProperties.ContainsKey(component)) {
				componentProperties[component] = new List<PropertyInfo>();
			}
			components.Add(component);

			var type = componentType;
			while (type != null) {
				var fields = type.GetFields(MemberFlags);
				var properties = type.GetProperties(MemberFlags);

				componentFields[component].AddRange(fields.Where(item => (item.FieldType.IsValueType && 
                                                                          !item.FieldType.IsPointer &&
                                                                          TypeFilter.Contains(item.FieldType))));
                                                                          
				componentProperties[component].AddRange(properties.Where(item => (item.PropertyType.IsValueType && 
                                                                                  !item.PropertyType.IsPointer && 
                                                                                  item.CanRead && 
                                                                                  item.CanWrite &&
                                                                                  TypeFilter.Contains(item.PropertyType))));
				type = type.BaseType;
				break;
			}
		}
	}

	private void CreateGenericMenu(List<TransformEase.EaseValue> list)
	{
		menu = new GenericMenu();

        foreach (var component in components) {
			foreach (var field in componentFields[component]) {
                var signature = new TransformEase.Signature(component, field);
                menu.AddItem(new GUIContent(FormatPath(component.GetType().Name, field.FieldType.Name, field.Name)),
                                            list.Find((item) => item.signature.Context == signature.Context &&
                                                                item.signature.ValueMember == signature.ValueMember) != null,
											(that) =>
											{
                                                var item = list.Find((item) => item.signature.Context == signature.Context &&
                                                                               item.signature.ValueMember == signature.ValueMember);
                                                if (item != null) {
                                                    list.Remove(item);
                                                }
                                                else {
                                                    list.Add(new TransformEase.EaseValue(signature, field.FieldType));
                                                }
											}, field);

			}
			foreach (var property in componentProperties[component]) {
                var signature = new TransformEase.Signature(component, property);
                menu.AddItem(new GUIContent(FormatPath(component.GetType().Name, property.PropertyType.Name, property.Name)),
                                            list.Find((item) => item.signature.Context == signature.Context &&
                                                                item.signature.ValueMember == signature.ValueMember) != null,
                                            (that) =>
                                            {
                                                var item = list.Find((item) => item.signature.Context == signature.Context &&
                                                                               item.signature.ValueMember == signature.ValueMember);
                                                if (item != null) {
                                                    list.Remove(item);
                                                }
                                                else {
                                                    list.Add(new TransformEase.EaseValue(signature, property.PropertyType));
                                                }
                                            }, property);
            }
		}
	}

	private void OnDisable()
	{
        serializedObject.ApplyModifiedProperties();
    }

    private string FormatPath(string componentName, string typeName, string memberName)
    {
        IEnumerable<string> StringToWordsWithSpace(string that) {
            return Regex.Matches(that, @"([A-Z]([0-9]|[a-z])+)").
                         Cast<Match>().
                         Select(m => m.Value);
        }
        var title = string.Format("{0}/{1}/{2}", string.Join(" ", StringToWordsWithSpace(char.ToUpper(componentName[0]) + componentName.Substring(1))),
                                            string.Join(" ", StringToWordsWithSpace(char.ToUpper(typeName[0]) + typeName.Substring(1))),
                                            string.Join(" ", StringToWordsWithSpace(char.ToUpper(memberName[0]) + memberName.Substring(1))));
        return title;
	}

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var list = ease.GetType().GetField("eases", MemberFlags)?.GetValue(ease) as List<TransformEase.EaseValue>;
        if (list == null) {
            list = new List<TransformEase.EaseValue>();
            ease.GetType().GetField("eases", MemberFlags)?.SetValue(ease, list);
        }
        EditorGUILayout.Space(60.0f);

        const float removeWidth = 60.0f;
        // 40 = label for value
        // 20 = padding
        var width = (EditorGUIUtility.currentViewWidth - removeWidth - 40.0f - 20.0f) / 2;


        object DrawMemberValue(string label, object value)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.MaxWidth(40.0f));
            var type = value.GetType();
            if (type == typeof(Vector3)) {
                value = EditorGUILayout.Vector3Field(string.Empty, (Vector3)value, GUILayout.MaxWidth(width));
            }
            else if (type == typeof(Vector2)) {
                value = EditorGUILayout.Vector2Field(string.Empty, (Vector2)value, GUILayout.MaxWidth(width));
            }
            else if (type == typeof(float)) {
                value = EditorGUILayout.FloatField(string.Empty, (float)value, GUILayout.MaxWidth(width));
            }
            else if (type == typeof(Color)) {
                value = EditorGUILayout.ColorField((Color)value, GUILayout.MaxWidth(width));
            }
            EditorGUILayout.EndHorizontal();
            return value;
        }

        CreateGenericMenu(list);
        TransformEase.EaseValue remove = null;
        foreach (var element in list) {
            EditorGUILayout.BeginHorizontal();
            var style = GUILayout.MaxWidth(width);

            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("delete", GUILayout.MaxWidth(removeWidth))) {
                remove = element;
            }
            EditorGUILayout.EndVertical();
            
            var context = element.signature.Context;
            if (element.signature.ValueMember is FieldInfo) {
                var field = element.signature.ValueMember as FieldInfo;
                EditorGUILayout.LabelField(new GUIContent(FormatPath(field.DeclaringType.Name,
                                                                     field.FieldType.Name,
                                                                     field.Name).
                                                                        Replace('/', ' ')),
                                           style);
            }
            else if (element.signature.ValueMember is PropertyInfo) {
                var property = element.signature.ValueMember as PropertyInfo;
                EditorGUILayout.LabelField(new GUIContent(FormatPath(property.DeclaringType.Name,
                                                                     property.PropertyType.Name,
                                                                     property.Name).
                                                                        Replace('/', ' ')),
                                           style);
            }
            else { // ToString Fallback
                EditorGUILayout.LabelField(new GUIContent(element.signature.ValueMember.ToString()), style);
            }

            EditorGUILayout.BeginVertical();
            element.from = DrawMemberValue("From: ", element.from);
            element.to = DrawMemberValue("To: ", element.to);
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10.0f);
        }

        if (remove != null) {
            list.Remove(remove);
        }

        if (GUILayout.Button("Add Variable/Property")) {
            menu.ShowAsContext();
        }

        serializedObject.ApplyModifiedProperties();

    }
}

