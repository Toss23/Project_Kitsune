using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityPoints))]
public class AbilityPointsEditor : Editor
{
    private SerializedProperty _points;

    private void OnEnable()
    {
        _points = serializedObject.FindProperty("_points");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle boldStyle = new GUIStyle();
        boldStyle.normal.textColor = Color.white;
        boldStyle.fontStyle = FontStyle.Bold;

        if (_points.arraySize != 5)
            _points.arraySize = 5;

        SerializedProperty[] abilityPoint = new SerializedProperty[5];
        for (int i = 0; i < 5; i++)
            abilityPoint[i] = _points.GetArrayElementAtIndex(i);

        int width = 160;
        int height = 30;
        int space = 5;
        EditorGUILayout.LabelField("Ability Points", boldStyle);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Attack", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Ultimate", GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        abilityPoint[0].objectReferenceValue = EditorGUILayout.ObjectField(abilityPoint[0].objectReferenceValue, typeof(GameObject), true, GUILayout.Width(width), GUILayout.Height(height));
        GUILayout.Space(space);
        abilityPoint[4].objectReferenceValue = EditorGUILayout.ObjectField(abilityPoint[4].objectReferenceValue, typeof(GameObject), true, GUILayout.Width(width), GUILayout.Height(height));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        for (int i = 1; i < 4; i++)
        {
            EditorGUILayout.LabelField("Ability " + i, GUILayout.Width(width));
            GUILayout.Space(space);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        for (int i = 1; i < 4; i++)
        {
            abilityPoint[i].objectReferenceValue = EditorGUILayout.ObjectField(abilityPoint[i].objectReferenceValue, typeof(GameObject), true, GUILayout.Width(width), GUILayout.Height(height));
            GUILayout.Space(space);
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}