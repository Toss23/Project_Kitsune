using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    private AbilityData _abilityData;

    private SerializedProperty _name;
    private SerializedProperty _description;

    private SerializedProperty _fuseWithCaster;
    private SerializedProperty _rotateAroundCaster;
    private SerializedProperty _spawnOnNearestEnemy;
    private SerializedProperty _spawnRange;

    private SerializedProperty _scale;
    private SerializedProperty _haveDuration;
    private SerializedProperty _duration;

    private SerializedProperty _abilityProperties;

    protected int _maxLevel;
    protected string _openedTab = "Main";
    private string _propertyNameAdd;
    private string _propertyNameDelete;

    private void OnEnable()
    {
        _abilityData = (AbilityData)target;

        InitProperties();
    }

    protected virtual void InitProperties()
    {
        _name = serializedObject.FindProperty("_name");
        _description = serializedObject.FindProperty("_description");

        _fuseWithCaster = serializedObject.FindProperty("_fuseWithCaster");
        _rotateAroundCaster = serializedObject.FindProperty("_rotateAroundCaster");
        _spawnOnNearestEnemy = serializedObject.FindProperty("_spawnOnNearestEnemy");
        _spawnRange = serializedObject.FindProperty("_spawnRange");

        _scale = serializedObject.FindProperty("_scale");
        _haveDuration = serializedObject.FindProperty("_haveDuration");
        _duration = serializedObject.FindProperty("_duration");

        _abilityProperties = serializedObject.FindProperty("_abilityProperties");
    }

    public override void OnInspectorGUI()
    {
        _maxLevel = _abilityData.GetMaxLevel();

        GUIStyle boldStyle = new GUIStyle();
        boldStyle.normal.textColor = Color.white;
        boldStyle.fontStyle = FontStyle.Bold;

        int width = 100;

        serializedObject.Update();

        EditorGUILayout.LabelField("Main Data", boldStyle);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name", GUILayout.Width(width));
        _name.stringValue = EditorGUILayout.TextField(_name.stringValue, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Description", GUILayout.Width(width));
        _description.stringValue = EditorGUILayout.TextArea(_description.stringValue, GUILayout.Width(300), GUILayout.MinHeight(50));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Max Level", GUILayout.Width(width));
        EditorGUILayout.LabelField(_maxLevel.ToString(), GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Position", boldStyle);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Rotate Around Caster", GUILayout.Width(width + 30));
        _rotateAroundCaster.boolValue = EditorGUILayout.Toggle(_rotateAroundCaster.boolValue);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Fuse with Caster", GUILayout.Width(width + 30));
        _fuseWithCaster.boolValue = EditorGUILayout.Toggle(_fuseWithCaster.boolValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Spawn on Enemy", GUILayout.Width(width + 30));
        _spawnOnNearestEnemy.boolValue = EditorGUILayout.Toggle(_spawnOnNearestEnemy.boolValue);
        EditorGUILayout.EndHorizontal();

        if (_spawnOnNearestEnemy.boolValue)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spawn Range", GUILayout.Width(width + 30));
            _spawnRange.floatValue = EditorGUILayout.FloatField(_spawnRange.floatValue, GUILayout.Width(width));
            EditorGUILayout.EndHorizontal();

            if (_spawnRange.floatValue < 0)
            {
                _spawnRange.floatValue = 0;
            }
        }

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Values", boldStyle);

        InitPropertyArray(_scale, _duration);

        EditorGUILayout.BeginHorizontal();
        AddTabButton("Main", width);
        AdditionalTabButton(width);
        AddTabButton("Custom", width);
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10);

        if (_openedTab == "Main")
        {
            AddBoolField(_haveDuration, "Have Duration", width);

            // Values
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Scale", GUILayout.Width(width));
            GUILayout.Space(10);
            if (_haveDuration.boolValue == true)
            {
                EditorGUILayout.LabelField("Duration", GUILayout.Width(width));
            }
            EditorGUILayout.EndHorizontal();

            for (int level = 1; level <= _maxLevel + 1; level++)
            {
                EditorGUILayout.BeginHorizontal();

                AddLevelField(level, width);
                AddField<float>(_scale, level, 1, width);
                if (_haveDuration.boolValue == true)
                {
                    AddField<float>(_duration, level, 10, width);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        AdditionalTab(width);

        if (_openedTab == "Custom")
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
            GUILayout.Space(10);

            for (int i = 0; i < _abilityProperties.arraySize; i++)
            {
                SerializedProperty property = _abilityProperties.GetArrayElementAtIndex(i);
                SerializedProperty propertyName = property.FindPropertyRelative("_name");
                EditorGUILayout.LabelField(propertyName.stringValue, GUILayout.Width(width));
                GUILayout.Space(10);
            }

            if (_abilityProperties.arraySize < 5)
            {
                _propertyNameAdd = EditorGUILayout.TextField(_propertyNameAdd, GUILayout.Width(width));
                if (GUILayout.Button("+", GUILayout.Width(25)) & _propertyNameAdd != "")
                {
                    _abilityProperties.arraySize += 1;
                    SerializedProperty property = _abilityProperties.GetArrayElementAtIndex(_abilityProperties.arraySize - 1);
                    SerializedProperty propertyName = property.FindPropertyRelative("_name");
                    SerializedProperty propertyValues = property.FindPropertyRelative("_values");
                    propertyName.stringValue = _propertyNameAdd;
                    propertyValues.arraySize = 2;
                    _propertyNameAdd = "";
                }
            }
            EditorGUILayout.EndHorizontal();

            for (int level = 1; level <= _maxLevel + 1; level++)
            {
                EditorGUILayout.BeginHorizontal();

                AddLevelField(level, width);

                for (int i = 0; i < _abilityProperties.arraySize; i++)
                {
                    SerializedProperty property = _abilityProperties.GetArrayElementAtIndex(i);
                    SerializedProperty propertyValues = property.FindPropertyRelative("_values");
                    InitPropertyArray(propertyValues);
                    AddField<float>(propertyValues, level, 0, width);
                }

                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Delete", GUILayout.Width(width / 2));
            GUILayout.Space(10);
            _propertyNameDelete = EditorGUILayout.TextField(_propertyNameDelete, GUILayout.Width(width));
            if (GUILayout.Button("Confirm", GUILayout.Width(100)))
            {
                for (int i = 0; i < _abilityProperties.arraySize; i++)
                {
                    SerializedProperty property = _abilityProperties.GetArrayElementAtIndex(i);
                    SerializedProperty propertyName = property.FindPropertyRelative("_name");

                    if (propertyName.stringValue == _propertyNameDelete)
                    {
                        _abilityProperties.DeleteArrayElementAtIndex(i);
                    }
                }

                _propertyNameDelete = "";
            }
            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

    protected void AddBoolField(SerializedProperty serializedProperty, string name, int width)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(name, GUILayout.Width(width));
        serializedProperty.boolValue = EditorGUILayout.Toggle(serializedProperty.boolValue);
        EditorGUILayout.EndHorizontal();
    }

    protected void AddFloatField(SerializedProperty serializedProperty, string name, int width)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(name, GUILayout.Width(width));
        serializedProperty.floatValue = EditorGUILayout.FloatField(serializedProperty.floatValue, GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();
    }

    protected void AddTabButton(string name, int width)
    {
        Color baseColor = GUI.backgroundColor;

        if (_openedTab == name)
        {
            GUI.backgroundColor = Color.cyan;
        }

        if (GUILayout.Button(name, GUILayout.Width(width)))
        {
            _openedTab = name;
        }

        GUI.backgroundColor = baseColor;
    }

    protected void ChangeArrayWithButtons<T>(SerializedProperty serializedProperty, float defaultValue, int width)
    {
        EditorGUILayout.BeginHorizontal(GUILayout.Width(width));

        if (GUILayout.Button("+"))
        {
            serializedProperty.arraySize++;
            SerializedProperty lastValue = serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1);
            if (typeof(T) == typeof(int))
            {
                lastValue.intValue = (int)defaultValue;
            }
            if (typeof(T) == typeof(float))
            {
                lastValue.floatValue = defaultValue;
            }
        }

        if (serializedProperty.arraySize > 2)
        {
            if (GUILayout.Button("-"))
            {
                serializedProperty.arraySize--;
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    protected void AddColumns(int width, params string[] names)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Level", GUILayout.Width(width / 2));
        GUILayout.Space(10);
        for (int i = 0; i < names.Length; i++)
        {
            EditorGUILayout.LabelField(names[i], GUILayout.Width(width));
            GUILayout.Space(10);
        }
        EditorGUILayout.EndHorizontal();
    }

    protected void AddLevelField(int i, int width)
    {
        if (i != _maxLevel + 1)
        {
            EditorGUILayout.LabelField("    " + i.ToString(), GUILayout.Width(width / 2));
        }
        else
        {
            GUILayout.Space(width / 2 + 3);
        }
        GUILayout.Space(10);
    }

    protected void AddField<T>(SerializedProperty array, int i, float defaultValue, int width)
    {
        if (i < array.arraySize)
        {
            SerializedProperty element = array.GetArrayElementAtIndex(i);
            if (typeof(T) == typeof(float))
            {
                element.floatValue = EditorGUILayout.FloatField(element.floatValue, GUILayout.Width(width));
            }
            else if (typeof(T) == typeof(int))
            {
                element.intValue = EditorGUILayout.IntField(element.intValue, GUILayout.Width(width));
            }
        }
        else if (i == _maxLevel + 1)
        {
            ChangeArrayWithButtons<T>(array, defaultValue, width);
        }
        else
        {
            GUILayout.Space(width + 3);
        }
        GUILayout.Space(10);
    }

    protected virtual void AdditionalTabButton(int width) { }

    protected virtual void AdditionalTab(int width) { }

    protected void InitPropertyArray(params SerializedProperty[] serializedProperties)
    {
        foreach (SerializedProperty serializedProperty in serializedProperties)
        {
            if (serializedProperty.arraySize < 2)
            {
                serializedProperty.arraySize = 2;
            }
        }
    }
}
