using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseAbilityData))]
public class BaseAbilityDataEditor : AbilityDataEditor
{
    private SerializedProperty _useCharacterDamage;
    private SerializedProperty _useCharacterCrit;

    private SerializedProperty _damage;
    private SerializedProperty _damageMultiplier;
    private SerializedProperty _castPerSecond;
    private SerializedProperty _critChance;
    private SerializedProperty _critMultiplier;
    private SerializedProperty _dotRate;

    protected override void InitProperties()
    {
        base.InitProperties();

        //
        _useCharacterDamage = serializedObject.FindProperty("_useCharacterDamage");
        _useCharacterCrit = serializedObject.FindProperty("_useCharacterCrit");

        _damage = serializedObject.FindProperty("_damage");
        _damageMultiplier = serializedObject.FindProperty("_damageMultiplier");
        _castPerSecond = serializedObject.FindProperty("_castPerSecond");
        _critChance = serializedObject.FindProperty("_critChance");
        _critMultiplier = serializedObject.FindProperty("_critMultiplier");
        _dotRate = serializedObject.FindProperty("_dotRate");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    protected override void AdditionalTabButton(int width)
    {
        base.AdditionalTabButton(width);
        
        //
        AddTabButton("Damage", width);
        AddTabButton("Crit Damage", width);
    }

    protected override void AdditionalTab(int width)
    {
        base.AdditionalTab(width);

        //
        InitPropertyArray(_damage, _damageMultiplier, _castPerSecond, _critChance, _critMultiplier, _dotRate);

        if (_openedTab == "Damage")
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Character Damage", GUILayout.Width(width + 50));
            _useCharacterDamage.boolValue = EditorGUILayout.Toggle(_useCharacterDamage.boolValue);
            EditorGUILayout.EndHorizontal();

            AddColumns(width, "Damage", "Multiplier", "Cast per Second", "Dot Rate");
            for (int level = 1; level <= _maxLevel + 1; level++)
            {
                EditorGUILayout.BeginHorizontal();
                AddLevelField(level, width);
                AddField(_damage, level, width);
                AddField(_damageMultiplier, level, width);
                AddField(_castPerSecond, level, width);
                AddField(_dotRate, level, width);
                EditorGUILayout.EndHorizontal();
            }
        }

        if (_openedTab == "Crit Damage")
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Character Crit", GUILayout.Width(width + 50));
            _useCharacterCrit.boolValue = EditorGUILayout.Toggle(_useCharacterCrit.boolValue);
            EditorGUILayout.EndHorizontal();

            AddColumns(width, "Crit Chance", "Crit Multiplier");
            for (int level = 1; level <= _maxLevel + 1; level++)
            {
                EditorGUILayout.BeginHorizontal();
                AddLevelField(level, width);
                AddField(_critChance, level, width);
                AddField(_critMultiplier, level, width);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
