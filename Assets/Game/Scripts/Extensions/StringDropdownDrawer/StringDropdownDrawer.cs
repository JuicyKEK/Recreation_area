using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Extensions.StringDropdownDrawer
{
    [CustomPropertyDrawer(typeof(StringDropdownAttribute))]
    public class StringDropdownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var dropdown = attribute as StringDropdownAttribute;
        
            // Ищем массив относительно родительского объекта (для вложенных классов)
            SerializedProperty arrayProp = FindSiblingProperty(property, dropdown.ArrayFieldName);
        
            if (arrayProp != null && arrayProp.isArray)
            {
                string[] options = new string[arrayProp.arraySize];
                for (int i = 0; i < arrayProp.arraySize; i++)
                {
                    options[i] = arrayProp.GetArrayElementAtIndex(i).stringValue;
                }
            
                if (options.Length > 0)
                {
                    int currentIndex = Mathf.Max(0, System.Array.IndexOf(options, property.stringValue));
                    int newIndex = EditorGUI.Popup(position, label.text, currentIndex, options);
                    property.stringValue = options[newIndex];
                }
                else
                {
                    EditorGUI.LabelField(position, label.text, "Array is empty");
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
                Debug.LogWarning($"Array '{dropdown.ArrayFieldName}' not found");
            }
        }
    
        /// <summary>
        /// Находит свойство-"соседа" в том же классе (работает для вложенных классов)
        /// </summary>
        private SerializedProperty FindSiblingProperty(SerializedProperty property, string siblingName)
        {
            string path = property.propertyPath;
            int lastDot = path.LastIndexOf('.');
        
            // Формируем путь к соседнему свойству
            string siblingPath = lastDot >= 0 
                ? path.Substring(0, lastDot + 1) + siblingName  // вложенный класс
                : siblingName;                                    // корневой уровень
        
            return property.serializedObject.FindProperty(siblingPath);
        }
    }
}