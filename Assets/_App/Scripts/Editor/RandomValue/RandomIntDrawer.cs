using UnityEditor;
using UnityEngine;
using VgGames.Core.RandomValue;

namespace VgGames.Editor.RandomValue
{
    [CustomPropertyDrawer(typeof(RandomInt))]
    public class RandomIntDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var center = position.width / 2;
            var startLabelRect = new Rect(position.x + 5, position.y, 30, position.height);
            var startRect = new Rect(position.x + 35, position.y,  center - 35, position.height);
            var endLabelRect = new Rect(position.x + center + 5, position.y, 30, position.height);
            var endRect = new Rect(position.x + center + 35, position.y, center - 35, position.height);

            EditorGUI.LabelField(startLabelRect, "Min");
            EditorGUI.PropertyField(startRect, property.FindPropertyRelative("min"), GUIContent.none);
            EditorGUI.LabelField(endLabelRect, "Max");
            EditorGUI.PropertyField(endRect, property.FindPropertyRelative("max"), GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}