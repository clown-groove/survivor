using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyWithAmmount))]
public class EnemyWithAmountDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var enemy = property.FindPropertyRelative("enemy");
        var amount = property.FindPropertyRelative("amount");

        float spacing = 10f;
        float amountWidth = 90f;

        Rect enemyRect = new Rect(
            position.x,
            position.y,
            position.width - amountWidth - spacing,
            EditorGUIUtility.singleLineHeight
        );

        Rect amountRect = new Rect(
            enemyRect.xMax + spacing,
            position.y,
            amountWidth,
            EditorGUIUtility.singleLineHeight
        );

        // Prefab
        EditorGUI.PropertyField(
            enemyRect,
            enemy,
            new GUIContent("Enemy")
        );

        // Liczba bez podpisu — więcej miejsca
        amount.intValue = EditorGUI.IntField(
            amountRect,
            amount.intValue
        );

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(
        SerializedProperty property,
        GUIContent label
    )
    {
        return EditorGUIUtility.singleLineHeight + 2;
    }
}