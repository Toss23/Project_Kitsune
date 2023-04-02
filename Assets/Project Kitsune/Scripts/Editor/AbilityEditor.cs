using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ability), true)]
public class AbilityEditor : Editor
{
    private SerializedProperty _useCharacterDamage;
    private SerializedProperty _useCharacterCrit;
    private SerializedProperty _abilityType;
    private SerializedProperty _damage;
    private SerializedProperty _damageMultiplier;
    private SerializedProperty _critChance;
    private SerializedProperty _critMultiplier;

    private void OnEnable()
    {
        _useCharacterDamage = serializedObject.FindProperty("_useCharacterDamage");
        _useCharacterCrit = serializedObject.FindProperty("_useCharacterCrit");
        _abilityType = serializedObject.FindProperty("_abilityType");
        _damage = serializedObject.FindProperty("_damage");
        _damageMultiplier = serializedObject.FindProperty("_damageMultiplier");
        _critChance = serializedObject.FindProperty("_critChance");
        _critMultiplier = serializedObject.FindProperty("_critMultiplier");
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
        Color baseColor = GUI.backgroundColor;
        if (_abilityType.enumValueIndex == 0)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Hit damage", GUILayout.Width(150)))
            _abilityType.enumValueIndex = 0;
        GUI.backgroundColor = baseColor;
        if (_abilityType.enumValueIndex == 1)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Damage over time", GUILayout.Width(150)))
            _abilityType.enumValueIndex = 1;
        GUI.backgroundColor = baseColor;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Damage");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max Level", GUILayout.Width(100));
        _damage.arraySize = EditorGUILayout.IntField(_damage.arraySize - 1, GUILayout.Width(50)) + 1;
        if (_damageMultiplier.arraySize != _damage.arraySize)
            _damageMultiplier.arraySize = _damage.arraySize;
        if (_critChance.arraySize != _damage.arraySize)
            _critChance.arraySize = _damage.arraySize;
        if (_critMultiplier.arraySize != _damage.arraySize)
            _critMultiplier.arraySize = _damage.arraySize;
        EditorGUILayout.EndHorizontal();

        int width = 80;
        int space = 5;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Damage", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Muliplier", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Crit Chance", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Crit Multiplier", GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();

        for (int i = 1; i < _damage.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _damageValue = _damage.GetArrayElementAtIndex(i);
            _damageValue.floatValue = EditorGUILayout.FloatField(_damageValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _damageMultiplierValue = _damageMultiplier.GetArrayElementAtIndex(i);
            _damageMultiplierValue.floatValue = EditorGUILayout.FloatField(_damageMultiplierValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _critChanceValue = _critChance.GetArrayElementAtIndex(i);
            _critChanceValue.floatValue = EditorGUILayout.FloatField(_critChanceValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _critMultiplierValue = _critMultiplier.GetArrayElementAtIndex(i);
            _critMultiplierValue.floatValue = EditorGUILayout.FloatField(_critMultiplierValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}