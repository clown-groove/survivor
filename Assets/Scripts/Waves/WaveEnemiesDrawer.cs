using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WaveEnemies))]
public class WaveEnemiesDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var enemies = property.FindPropertyRelative("enemies");

        EditorGUI.BeginProperty(position, label, property);

        float lineH = EditorGUIUtility.singleLineHeight;
        float y = position.y;

        // HEADER
        int index = GetIndex(property);
        EditorGUI.LabelField(
            new Rect(position.x, y, position.width, lineH),
            $"Wave {index + 1}",
            EditorStyles.boldLabel
        );

        y += lineH + 4;

        // LIST
        for (int i = 0; i < enemies.arraySize; i++)
        {
            var element = enemies.GetArrayElementAtIndex(i);

            Rect row = new Rect(position.x, y, position.width - 25, lineH);
            Rect delBtn = new Rect(position.x + position.width - 22, y, 22, lineH);

            EditorGUI.PropertyField(row, element, GUIContent.none);

            if (GUI.Button(delBtn, "X"))
            {
                enemies.DeleteArrayElementAtIndex(i);
                break;
            }

            y += lineH + 2;
        }

        // ADD BUTTON
        Rect addBtn = new Rect(position.x, y, position.width, lineH);

        if (GUI.Button(addBtn, "+ Add Enemy"))
        {
            enemies.arraySize++;
            var newElement = enemies.GetArrayElementAtIndex(enemies.arraySize - 1);

            // opcjonalne defaulty
            newElement.FindPropertyRelative("amount").intValue = 1;
            newElement.FindPropertyRelative("enemy").objectReferenceValue = null;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var enemies = property.FindPropertyRelative("enemies");

        float lineH = EditorGUIUtility.singleLineHeight;

        return lineH + // title
               (enemies.arraySize * (lineH + 2)) +
               lineH + 6; // button
    }

    private int GetIndex(SerializedProperty property)
    {
        string path = property.propertyPath;
        string[] parts = path.Split('[', ']');

        if (parts.Length > 1 && int.TryParse(parts[1], out int index))
            return index;

        return 0;
    }
}