using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityInfo), true)]
public class AbilityInfoEditor : Editor
{
    private enum PropertyTab
    {
        Damage, Projectile, DamageOverTime, Other
    }

    private SerializedProperty _useCharacterDamage;
    private SerializedProperty _useCharacterCrit;

    private SerializedProperty _abilityDamageType;
    private SerializedProperty _dotRate;
    private SerializedProperty _dotDuration;

    private SerializedProperty _abilityType;
    private SerializedProperty _meleeAnimationTime;
    private SerializedProperty _projectileSpeed;
    private SerializedProperty _projectileRange;
    private SerializedProperty _projectileCount;
    private SerializedProperty _projectileSplitAngle;
    private SerializedProperty _projectileAuto;
    private SerializedProperty _destroyOnHit;

    private SerializedProperty _haveContinueAbility;
    private SerializedProperty _continueAbility;

    private SerializedProperty _haveAura;
    private SerializedProperty _auraObject;

    private SerializedProperty _damage;
    private SerializedProperty _damageMultiplier;
    private SerializedProperty _castPerSecond;

    private SerializedProperty _critChance;
    private SerializedProperty _critMultiplier;

    private SerializedProperty _name;
    private SerializedProperty _description;

    private SerializedProperty _radius;

    private PropertyTab _propertyTab = PropertyTab.Damage;

    private void OnEnable()
    {
        _useCharacterDamage = serializedObject.FindProperty("_useCharacterDamage");
        _useCharacterCrit = serializedObject.FindProperty("_useCharacterCrit");

        _abilityType = serializedObject.FindProperty("_abilityType");
        _dotRate = serializedObject.FindProperty("_dotRate");
        _dotDuration = serializedObject.FindProperty("_dotDuration");

        _abilityDamageType = serializedObject.FindProperty("_abilityDamageType");
        _meleeAnimationTime = serializedObject.FindProperty("_meleeAnimationTime");
        _projectileSpeed = serializedObject.FindProperty("_projectileSpeed");
        _projectileRange = serializedObject.FindProperty("_projectileRange");
        _projectileCount = serializedObject.FindProperty("_projectileCount");
        _projectileSplitAngle = serializedObject.FindProperty("_projectileSplitAngle");
        _projectileAuto = serializedObject.FindProperty("_projectileAuto");
        _destroyOnHit = serializedObject.FindProperty("_destroyOnHit");

        _haveContinueAbility = serializedObject.FindProperty("_haveContinueAbility");
        _continueAbility = serializedObject.FindProperty("_continueAbility");

        _haveAura = serializedObject.FindProperty("_haveAura");
        _auraObject = serializedObject.FindProperty("_auraObject");

        _damage = serializedObject.FindProperty("_damage");
        _damageMultiplier = serializedObject.FindProperty("_damageMultiplier");
        _castPerSecond = serializedObject.FindProperty("_castPerSecond");

        _critChance = serializedObject.FindProperty("_critChance");
        _critMultiplier = serializedObject.FindProperty("_critMultiplier");

        _name = serializedObject.FindProperty("_name");
        _description = serializedObject.FindProperty("_description");

        _radius = serializedObject.FindProperty("_radius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle boldStyle = new GUIStyle();
        boldStyle.normal.textColor = Color.white;
        boldStyle.fontStyle = FontStyle.Bold;

        EditorGUILayout.LabelField("Main", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name", GUILayout.Width(150));
        _name.stringValue = EditorGUILayout.TextField(_name.stringValue, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Character Damage", GUILayout.Width(150));
        _useCharacterDamage.boolValue = EditorGUILayout.Toggle(_useCharacterDamage.boolValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Use Character Crit", GUILayout.Width(150));
        _useCharacterCrit.boolValue = EditorGUILayout.Toggle(_useCharacterCrit.boolValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Description");
        _description.stringValue = EditorGUILayout.TextArea(_description.stringValue, GUILayout.Width(500), GUILayout.MinHeight(40));
        
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Damage Type", boldStyle);
        EditorGUILayout.BeginHorizontal();
        Color baseColor = GUI.backgroundColor;

        if (_abilityType.enumValueIndex == 0)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Melee", GUILayout.Width(150)))
            _abilityType.enumValueIndex = 0;
        GUI.backgroundColor = baseColor;

        if (_abilityType.enumValueIndex == 1)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Projectile", GUILayout.Width(150)))
            _abilityType.enumValueIndex = 1;
        GUI.backgroundColor = baseColor;

        if (_abilityType.enumValueIndex == 2)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Field", GUILayout.Width(150)))
            _abilityType.enumValueIndex = 2;
        GUI.backgroundColor = baseColor;
        EditorGUILayout.EndHorizontal();

        if (_abilityType.enumValueIndex == 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Animation time", GUILayout.Width(100));
            _meleeAnimationTime.floatValue = EditorGUILayout.FloatField(_meleeAnimationTime.floatValue, GUILayout.Width(70));
            EditorGUILayout.EndHorizontal();
        }

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

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Destroy on Hit", GUILayout.Width(100));
            _destroyOnHit.boolValue = EditorGUILayout.Toggle(_destroyOnHit.boolValue);
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Space(5);

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
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Continue Ability", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Have continue", GUILayout.Width(100));
        _haveContinueAbility.boolValue = EditorGUILayout.Toggle(_haveContinueAbility.boolValue);
        EditorGUILayout.EndHorizontal();
        if (_haveContinueAbility.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ability", GUILayout.Width(100));
            _continueAbility.objectReferenceValue = EditorGUILayout.ObjectField(_continueAbility.objectReferenceValue, typeof(Ability), false, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Have Aura", GUILayout.Width(100));
        _haveAura.boolValue = EditorGUILayout.Toggle(_haveAura.boolValue);
        EditorGUILayout.EndHorizontal();
        if (_haveAura.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Aura", GUILayout.Width(100));
            _auraObject.objectReferenceValue = EditorGUILayout.ObjectField(_auraObject.objectReferenceValue, typeof(GameObject), false, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Properties", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max Level", GUILayout.Width(100));
        _damage.arraySize = EditorGUILayout.IntField(_damage.arraySize - 1, GUILayout.Width(50)) + 1;
        if (_damageMultiplier.arraySize != _damage.arraySize)
        {
            int prevSize = _damageMultiplier.arraySize;
            _damageMultiplier.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _damageMultiplier.arraySize; i++)
            {
                SerializedProperty element = _damageMultiplier.GetArrayElementAtIndex(i);
                element.floatValue = 100;
            }
        }

        if (_castPerSecond.arraySize != _damage.arraySize)
        {
            int prevSize = _castPerSecond.arraySize;
            _castPerSecond.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _castPerSecond.arraySize; i++)
            {
                SerializedProperty element = _castPerSecond.GetArrayElementAtIndex(i);
                element.floatValue = 1;
            }
        }

        if (_critChance.arraySize != _damage.arraySize)
        {
            int prevSize = _critChance.arraySize;
            _critChance.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _critChance.arraySize; i++)
            {
                SerializedProperty element = _critChance.GetArrayElementAtIndex(i);
                element.floatValue = 0;
            }
        }

        if (_critMultiplier.arraySize != _damage.arraySize)
        {
            int prevSize = _critMultiplier.arraySize;
            _critMultiplier.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _damageMultiplier.arraySize; i++)
            {
                SerializedProperty element = _critMultiplier.GetArrayElementAtIndex(i);
                element.floatValue = 100;
            }
        }

        if (_projectileCount.arraySize != _damage.arraySize)
        {
            int prevSize = _projectileCount.arraySize;
            _projectileCount.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _projectileCount.arraySize; i++)
            {
                SerializedProperty element = _projectileCount.GetArrayElementAtIndex(i);
                element.intValue = 1;
            }
        }

        if (_projectileSplitAngle.arraySize != _damage.arraySize)
        {
            int prevSize = _projectileSplitAngle.arraySize;
            _projectileSplitAngle.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _projectileSplitAngle.arraySize; i++)
            {
                SerializedProperty element = _projectileSplitAngle.GetArrayElementAtIndex(i);
                element.floatValue = 0;
            }
        }

        if (_dotRate.arraySize != _damage.arraySize)
        {
            int prevSize = _dotRate.arraySize;
            _dotRate.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _dotRate.arraySize; i++)
            {
                SerializedProperty element = _dotRate.GetArrayElementAtIndex(i);
                element.floatValue = 1;
            }
        }

        if (_dotDuration.arraySize != _damage.arraySize)
        {
            int prevSize = _dotDuration.arraySize;
            _dotDuration.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _dotDuration.arraySize; i++)
            {
                SerializedProperty element = _dotDuration.GetArrayElementAtIndex(i);
                element.floatValue = 5;
            }
        }

        if (_radius.arraySize != _damage.arraySize)
        {
            int prevSize = _radius.arraySize;
            _radius.arraySize = _damage.arraySize;
            for (int i = prevSize; i < _radius.arraySize; i++)
            {
                SerializedProperty element = _radius.GetArrayElementAtIndex(i);
                element.floatValue = 1;
            }
        }

        EditorGUILayout.EndHorizontal();

        int width = 80;
        int space = 5;

        GUILayout.Space(5);
        EditorGUILayout.BeginHorizontal();
        if (_propertyTab == PropertyTab.Damage)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Damage", GUILayout.Width(120)))
            _propertyTab = PropertyTab.Damage;

        GUI.backgroundColor = baseColor;
        if (_abilityType.enumValueIndex != 1)
        {
            if (_propertyTab == PropertyTab.Projectile)
                _propertyTab = PropertyTab.Damage;

            GUI.backgroundColor = Color.red;
        }

        if (_propertyTab == PropertyTab.Projectile)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Projectile", GUILayout.Width(120)) && _abilityType.enumValueIndex == 1)
            _propertyTab = PropertyTab.Projectile;

        GUI.backgroundColor = baseColor;
        if (_abilityDamageType.enumValueIndex != 1)
        {
            if (_propertyTab == PropertyTab.DamageOverTime)
                _propertyTab = PropertyTab.Damage;

            GUI.backgroundColor = Color.red;
        }

        if (_propertyTab == PropertyTab.DamageOverTime)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Damage Over Time", GUILayout.Width(120)) && _abilityDamageType.enumValueIndex == 1)
            _propertyTab = PropertyTab.DamageOverTime;

        GUI.backgroundColor = baseColor;
        if (_propertyTab == PropertyTab.Other)
            GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Other", GUILayout.Width(120)))
            _propertyTab = PropertyTab.Other;

        GUI.backgroundColor = baseColor;
        EditorGUILayout.EndHorizontal();

        if (_propertyTab == PropertyTab.Damage)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Damage", GUILayout.Width(width));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Muliplier", GUILayout.Width(width));
            GUILayout.Space(space);
            if (_abilityType.enumValueIndex != 2)
            {
                EditorGUILayout.LabelField("Cast per Sec", GUILayout.Width(width));
                GUILayout.Space(space);
            }
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

                if (_abilityType.enumValueIndex != 2)
                {
                    SerializedProperty _castPerSecondValue = _castPerSecond.GetArrayElementAtIndex(i);
                    _castPerSecondValue.floatValue = EditorGUILayout.FloatField(_castPerSecondValue.floatValue, GUILayout.Width(width));
                    GUILayout.Space(space);
                }

                SerializedProperty _critChanceValue = _critChance.GetArrayElementAtIndex(i);
                _critChanceValue.floatValue = EditorGUILayout.FloatField(_critChanceValue.floatValue, GUILayout.Width(width));
                GUILayout.Space(space);

                SerializedProperty _critMultiplierValue = _critMultiplier.GetArrayElementAtIndex(i);
                _critMultiplierValue.floatValue = EditorGUILayout.FloatField(_critMultiplierValue.floatValue, GUILayout.Width(width));
                GUILayout.Space(space);
                EditorGUILayout.EndHorizontal();
            }
        }

        if (_propertyTab == PropertyTab.Projectile)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Count", GUILayout.Width(width));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Split Angle", GUILayout.Width(width));
            EditorGUILayout.EndHorizontal();

            for (int i = 1; i < _damage.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(25));
                GUILayout.Space(space);

                SerializedProperty _projectileCountValue = _projectileCount.GetArrayElementAtIndex(i);
                _projectileCountValue.intValue = EditorGUILayout.IntField(_projectileCountValue.intValue, GUILayout.Width(width));
                GUILayout.Space(space);

                SerializedProperty _projectileSplitAngleValue = _projectileSplitAngle.GetArrayElementAtIndex(i);
                _projectileSplitAngleValue.floatValue = EditorGUILayout.FloatField(_projectileSplitAngleValue.floatValue, GUILayout.Width(width));
                GUILayout.Space(space);
                
                EditorGUILayout.EndHorizontal();
            }
        }

        if (_propertyTab == PropertyTab.DamageOverTime)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Rate", GUILayout.Width(width));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Duration", GUILayout.Width(width));
            EditorGUILayout.EndHorizontal();

            for (int i = 1; i < _damage.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(25));
                GUILayout.Space(space);

                SerializedProperty _dotRateElement = _dotRate.GetArrayElementAtIndex(i);
                _dotRateElement.floatValue = EditorGUILayout.FloatField(_dotRateElement.floatValue, GUILayout.Width(width));
                GUILayout.Space(space);

                if (_abilityType.enumValueIndex != 2)
                {
                    SerializedProperty _dotDurationElement = _dotDuration.GetArrayElementAtIndex(i);
                    _dotDurationElement.floatValue = EditorGUILayout.FloatField(_dotDurationElement.floatValue, GUILayout.Width(width));
                }
                else
                {
                    EditorGUILayout.LabelField("Infinity", GUILayout.Width(width));
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        if (_propertyTab == PropertyTab.Other)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
            GUILayout.Space(space);
            EditorGUILayout.LabelField("Radius", GUILayout.Width(width));
            EditorGUILayout.EndHorizontal();

            for (int i = 1; i < _damage.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(25));
                GUILayout.Space(space);

                SerializedProperty _radiusElement = _radius.GetArrayElementAtIndex(i);
                _radiusElement.floatValue = EditorGUILayout.FloatField(_radiusElement.floatValue, GUILayout.Width(width));
                GUILayout.Space(space);

                EditorGUILayout.EndHorizontal();
            }
        }

        GUILayout.Space(20);

        EditorGUILayout.LabelField("Formula", boldStyle);
        EditorGUILayout.LabelField("Damage = (ADamage + CDamage) * ADamageMultiplier * Crit");

        serializedObject.ApplyModifiedProperties();
    }
}