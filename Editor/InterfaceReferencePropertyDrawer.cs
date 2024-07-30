using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Fofanius.Type.Editor
{
    [CustomPropertyDrawer(typeof(InterfaceReference<>))]
    public class InterfaceReferencePropertyDrawer : PropertyDrawer
    {
        private const float DEFAULT_HEIGHT = 18f;
        private const string OBJECT_PROPERTY_KEY = "_object";

        private static readonly Color _validReferenceColor = new Color(0.18f, 1f, 0.46f);
        private static readonly Color _invalidReferenceColor = new Color(1f, 0.31f, 0.18f);
        private static readonly Color _emptyReferenceColor = new Color(0.44f, 0.87f, 0.98f);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => DEFAULT_HEIGHT * 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            System.Type requiredType = default;

            if (fieldInfo.FieldType.IsGenericType)
            {
                // InterfaceReference<>
                requiredType = fieldInfo.FieldType.GetGenericArguments()[0];

                // IEnumerable<InterfaceReference<>> e.g. List<InterfaceReference<>>
                foreach (var genericArgument in fieldInfo.FieldType.GetGenericArguments())
                {
                    var genericEnumerable = typeof(IEnumerable<>).MakeGenericType(genericArgument);
                    if (genericEnumerable.IsAssignableFrom(fieldInfo.FieldType))
                    {
                        requiredType = genericArgument.GetGenericArguments()[0];
                        break;
                    }
                }
            }
            //  InterfaceReference<>[]
            else if (fieldInfo.FieldType.IsArray)
            {
                requiredType = fieldInfo.FieldType.GetElementType()!.GetGenericArguments()[0];
            }

            if (requiredType is null)
            {
                position.height = DEFAULT_HEIGHT;
                GUI.color = Color.red;
                EditorGUI.LabelField(position, "Unable to draw for type:");
                position.y += DEFAULT_HEIGHT;
                EditorGUI.LabelField(position, fieldInfo.FieldType.Name);
                return;
            }

            var objectProperty = property.FindPropertyRelative(OBJECT_PROPERTY_KEY);

            // object reference
            position.height = DEFAULT_HEIGHT;
            EditorGUI.PropertyField(position, objectProperty, new GUIContent(property.displayName));

            // filter reference
            if (objectProperty.objectReferenceValue is not (null or Component or ScriptableObject or GameObject))
            {
                objectProperty.objectReferenceValue = default;
            }
            else if (objectProperty.objectReferenceValue is GameObject gameObject)
            {
                objectProperty.objectReferenceValue = gameObject.GetComponent(requiredType);
            }

            // tooltip
            position.y += DEFAULT_HEIGHT;

            var c = GUI.color;
            EditorGUI.indentLevel++;
            if (objectProperty.objectReferenceValue)
            {
                var targetType = objectProperty.objectReferenceValue.GetType();
                GUI.color = targetType.GetInterfaces().Contains(requiredType) ? _validReferenceColor : _invalidReferenceColor;
                EditorGUI.LabelField(position, $"[{requiredType.Name}] {objectProperty.objectReferenceValue.GetType().Name}");
            }
            else
            {
                GUI.color = _emptyReferenceColor;
                EditorGUI.LabelField(position, $"[{requiredType.Name}] *null or missing*", EditorStyles.miniLabel);
            }

            EditorGUI.indentLevel--;
            GUI.color = c;
        }
    }
}