using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ability), true)]
public class AbilityEditor : Editor
{
    private SerializedProperty _useCharacterDamage;
    private SerializedProperty _useCharacterCrit;
    private SerializedProperty _abilityType;
    private SerializedProperty _damage;

    private void OnEnable()
    {
        _useCharacterDamage = serializedObject.FindProperty("_useCharacterDamage");
        _useCharacterCrit = serializedObject.FindProperty("_useCharacterCrit");
        _abilityType = serializedObject.FindProperty("_abilityType");
        _damage = serializedObject.FindProperty("_damage");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Using");
        _useCharacterDamage.boolValue = EditorGUILayout.Toggle("Use Character Damage", _useCharacterDamage.boolValue);
        _useCharacterCrit.boolValue = EditorGUILayout.Toggle("Use Character Crit", _useCharacterCrit.boolValue);
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Damage Type");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Hit damage"))
            _abilityType.enumValueIndex = 0;
        if (GUILayout.Button("Damage over time"))
            _abilityType.enumValueIndex = 1;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Damage");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max Level");
        _damage.arraySize = EditorGUILayout.IntField(_damage.arraySize);
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < _damage.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(i.ToString());
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}