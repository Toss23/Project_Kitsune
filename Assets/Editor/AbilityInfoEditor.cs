using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityInfo), true)]
public class AbilityInfoEditor : Editor
{
    private SerializedProperty _useCharacterDamage;
    private SerializedProperty _useCharacterCrit;

    private SerializedProperty _abilityDamageType;
    private SerializedProperty _dotRate;
    private SerializedProperty _dotDuration;

    private SerializedProperty _abilityType;
    private SerializedProperty _projectileSpeed;
    private SerializedProperty _projectileRange;
    private SerializedProperty _projectileAuto;

    private SerializedProperty _haveContinueAbility;
    private SerializedProperty _continueAbility;

    private SerializedProperty _damage;
    private SerializedProperty _damageMultiplier;
    private SerializedProperty _castPerSecond;
    private SerializedProperty _critChance;
    private SerializedProperty _critMultiplier;
    private SerializedProperty _description;

    private void OnEnable()
    {
        _useCharacterDamage = serializedObject.FindProperty("_useCharacterDamage");
        _useCharacterCrit = serializedObject.FindProperty("_useCharacterCrit");

        _abilityType = serializedObject.FindProperty("_abilityType");
        _dotRate = serializedObject.FindProperty("_dotRate");
        _dotDuration = serializedObject.FindProperty("_dotDuration");

        _abilityDamageType = serializedObject.FindProperty("_abilityDamageType");
        _projectileSpeed = serializedObject.FindProperty("_projectileSpeed");
        _projectileRange = serializedObject.FindProperty("_projectileRange");
        _projectileAuto = serializedObject.FindProperty("_projectileAuto");

        _haveContinueAbility = serializedObject.FindProperty("_haveContinueAbility");
        _continueAbility = serializedObject.FindProperty("_continueAbility");

        _damage = serializedObject.FindProperty("_damage");
        _damageMultiplier = serializedObject.FindProperty("_damageMultiplier");
        _castPerSecond = serializedObject.FindProperty("_castPerSecond");

        _critChance = serializedObject.FindProperty("_critChance");
        _critMultiplier = serializedObject.FindProperty("_critMultiplier");
        _description = serializedObject.FindProperty("_description");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle boldStyle = new GUIStyle();
        boldStyle.normal.textColor = Color.white;
        boldStyle.fontStyle = FontStyle.Bold;

        EditorGUILayout.LabelField("Main", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Character Damage", GUILayout.Width(150));
        _useCharacterDamage.boolValue = EditorGUILayout.Toggle(_useCharacterDamage.boolValue);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Character Crit", GUILayout.Width(150));
        _useCharacterCrit.boolValue = EditorGUILayout.Toggle(_useCharacterCrit.boolValue);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Description");
        _description.stringValue = EditorGUILayout.TextArea(_description.stringValue, GUILayout.Width(300), GUILayout.MinHeight(40));
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Damage Type", boldStyle);
        EditorGUILayout.BeginHorizontal();
        Color baseColor = GUI.backgroundColor;
        if (_abilityType.enumValueIndex == 0)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Melee or Target", GUILayout.Width(150)))
            _abilityType.enumValueIndex = 0;
        GUI.backgroundColor = baseColor;
        if (_abilityType.enumValueIndex == 1)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Projectile", GUILayout.Width(150)))
            _abilityType.enumValueIndex = 1;
        GUI.backgroundColor = baseColor;
        EditorGUILayout.EndHorizontal();

        if (_abilityType.enumValueIndex == 1)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Projectile Speed", GUILayout.Width(100));
            _projectileSpeed.floatValue = EditorGUILayout.FloatField(_projectileSpeed.floatValue, GUILayout.Width(70));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Projectile Range", GUILayout.Width(100));
            _projectileRange.floatValue = EditorGUILayout.FloatField(_projectileRange.floatValue, GUILayout.Width(70));
            EditorGUILayout.LabelField("sec", GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Auto-Target", GUILayout.Width(100));
            _projectileAuto.boolValue = EditorGUILayout.Toggle(_projectileAuto.boolValue);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        EditorGUILayout.BeginHorizontal();
        if (_abilityDamageType.enumValueIndex == 0)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Hit damage", GUILayout.Width(150)))
            _abilityDamageType.enumValueIndex = 0;
        GUI.backgroundColor = baseColor;
        if (_abilityDamageType.enumValueIndex == 1)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Damage over time", GUILayout.Width(150)))
            _abilityDamageType.enumValueIndex = 1;
        GUI.backgroundColor = baseColor;
        EditorGUILayout.EndHorizontal();

        if (_abilityDamageType.enumValueIndex == 1)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Damage Rate", GUILayout.Width(100));
            _dotRate.floatValue = EditorGUILayout.FloatField(_dotRate.floatValue, GUILayout.Width(70));
            EditorGUILayout.LabelField("sec per damage", GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Duration", GUILayout.Width(100));
            _dotDuration.floatValue = EditorGUILayout.FloatField(_dotDuration.floatValue, GUILayout.Width(70));
            EditorGUILayout.LabelField("sec", GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Continue Ability", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Have continue", GUILayout.Width(100));
        _haveContinueAbility.boolValue = EditorGUILayout.Toggle(_haveContinueAbility.boolValue);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ability", GUILayout.Width(100));
        _continueAbility.objectReferenceValue = EditorGUILayout.ObjectField(_continueAbility.objectReferenceValue, typeof(Ability), false, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Damage", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max Level", GUILayout.Width(100));
        _damage.arraySize = EditorGUILayout.IntField(_damage.arraySize - 1, GUILayout.Width(50)) + 1;
        if (_damageMultiplier.arraySize != _damage.arraySize)
        {
            int prevSize = _damageMultiplier.arraySize;
            _damageMultiplier.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _damageMultiplier.arraySize; i++)
            {
                SerializedProperty _damageMultiplierValue = _damageMultiplier.GetArrayElementAtIndex(i);
                _damageMultiplierValue.floatValue = 100;
            }
        }

        if (_castPerSecond.arraySize != _damage.arraySize)
        {
            int prevSize = _castPerSecond.arraySize;
            _castPerSecond.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _castPerSecond.arraySize; i++)
            {
                SerializedProperty _castPerSecondValue = _castPerSecond.GetArrayElementAtIndex(i);
                _castPerSecondValue.floatValue = 1;
            }
        }

        if (_critChance.arraySize != _damage.arraySize)
        {
            int prevSize = _critChance.arraySize;
            _critChance.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _critChance.arraySize; i++)
            {
                SerializedProperty _critChanceValue = _critChance.GetArrayElementAtIndex(i);
                _critChanceValue.floatValue = 0;
            }
        }

        if (_critMultiplier.arraySize != _damage.arraySize)
        {
            int prevSize = _critMultiplier.arraySize;
            _critMultiplier.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _damageMultiplier.arraySize; i++)
            {
                SerializedProperty _critMultiplierValue = _critMultiplier.GetArrayElementAtIndex(i);
                _critMultiplierValue.floatValue = 100;
            }
        }

        EditorGUILayout.EndHorizontal();

        int width = 80;
        int space = 5;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Damage", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Muliplier", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Cast per Sec", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Crit Chance", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Crit Multiplier", GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();

        for (int i = 1; i < _damage.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(25));
            GUILayout.Space(space);

            SerializedProperty _damageValue = _damage.GetArrayElementAtIndex(i);
            _damageValue.floatValue = EditorGUILayout.FloatField(_damageValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _damageMultiplierValue = _damageMultiplier.GetArrayElementAtIndex(i);
            _damageMultiplierValue.floatValue = EditorGUILayout.FloatField(_damageMultiplierValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _castPerSecondValue = _castPerSecond.GetArrayElementAtIndex(i);
            _castPerSecondValue.floatValue = EditorGUILayout.FloatField(_castPerSecondValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _critChanceValue = _critChance.GetArrayElementAtIndex(i);
            _critChanceValue.floatValue = EditorGUILayout.FloatField(_critChanceValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);

            SerializedProperty _critMultiplierValue = _critMultiplier.GetArrayElementAtIndex(i);
            _critMultiplierValue.floatValue = EditorGUILayout.FloatField(_critMultiplierValue.floatValue, GUILayout.Width(width));
            GUILayout.Space(space);
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(20);

        EditorGUILayout.LabelField("Formula", boldStyle);
        EditorGUILayout.LabelField("Damage = (ADamage + CDamage) * ADamageMultiplier * Crit");

        serializedObject.ApplyModifiedProperties();
    }
}