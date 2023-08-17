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
            AddBoolField(_useCharacterDamage, "Use Character Damage", width + 50);
            AddColumns(width, "Damage", "Multiplier", "Cast per Second", "Dot Rate");
            for (int level = 1; level <= _maxLevel + 1; level++)
            {
                EditorGUILayout.BeginHorizontal();
                AddLevelField(level, width);
                AddField<float>(_damage, level, width);
                AddField<float>(_damageMultiplier, level, width);
                AddField<float>(_castPerSecond, level, width);
                AddField<float>(_dotRate, level, width);
                EditorGUILayout.EndHorizontal();
            }
        }

        if (_openedTab == "Crit Damage")
        {
            AddBoolField(_useCharacterCrit, "Use Character Crit", width + 50);

            GUILayout.Space(10);

            AddColumns(width, "Crit Chance", "Crit Multiplier");
            for (int level = 1; level <= _maxLevel + 1; level++)
            {
                EditorGUILayout.BeginHorizontal();
                AddLevelField(level, width);
                AddField<float>(_critChance, level, width);
                AddField<float>(_critMultiplier, level, width);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
