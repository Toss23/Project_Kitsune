using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityPoints))]
public class AbilityPointsEditor : Editor
{
    private SerializedProperty _unitInfo;
    private SerializedProperty _points;

    private void OnEnable()
    {
        _unitInfo = serializedObject.FindProperty("_unitInfo");
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

        SerializedProperty[] abilityPoints = new SerializedProperty[5];
        for (int i = 0; i < 5; i++)
            abilityPoints[i] = _points.GetArrayElementAtIndex(i);

        int width = 160;
        int space = 5;
        EditorGUILayout.LabelField("Ability Points", boldStyle);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Unit Info", GUILayout.Width(60));
        _unitInfo.objectReferenceValue = EditorGUILayout.ObjectField(_unitInfo.objectReferenceValue, typeof(UnitInfo), false, GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();

        if (_unitInfo.objectReferenceValue != null)
        {
            bool[] haveAbility = new bool[5];
            bool[] haveAura = new bool[5];

            IAbility[] abilities = (_unitInfo.objectReferenceValue as UnitInfo).Abilities;
            for (int i = 0; i < 5; i++)
            {
                haveAbility[i] = abilities[i] != null && abilities[i].AbilityData != null;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(120));
            EditorGUILayout.LabelField("Point", GUILayout.Width(width));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Aura", GUILayout.Width(width));
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < 5; i++)
            {
                if (haveAbility[i])
                {
                    string text = "Ability " + i;
                    if (i == 0) text = "Attack";
                    if (i == 4) text = "Ultimate";

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(text, GUILayout.Width(60));
                    abilityPoints[i].objectReferenceValue = EditorGUILayout.ObjectField(abilityPoints[i].objectReferenceValue, typeof(GameObject), true, GUILayout.Width(width));
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(space);
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}