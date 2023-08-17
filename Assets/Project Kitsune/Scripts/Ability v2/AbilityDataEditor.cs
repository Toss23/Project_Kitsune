using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor
{
    private AbilityData _abilityData;

    private SerializedProperty _name;
    private SerializedProperty _description;

    private SerializedProperty _fuseWithCaster;
    private SerializedProperty _spawnOnNearestEnemy;

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
        _spawnOnNearestEnemy = serializedObject.FindProperty("_spawnOnNearestEnemy");
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
        EditorGUILayout.LabelField("Fuse with Caster", GUILayout.Width(width));
        _fuseWithCaster.boolValue = EditorGUILayout.Toggle(_fuseWithCaster.boolValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Spawn on Enemy", GUILayout.Width(width));
        _spawnOnNearestEnemy.boolValue = EditorGUILayout.Toggle(_spawnOnNearestEnemy.boolValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Values", boldStyle);

        InitPropertyArray(_scale, _duration);

        EditorGUILayout.BeginHorizontal();
        AddTabButton("Main", width);
        AdditionalTabButton(width);
        AddTabButton("Custom", width);
        EditorGUILayout.EndHorizontal();

        if (_openedTab == "Main")
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Have Duration", GUILayout.Width(width));
            _haveDuration.boolValue = EditorGUILayout.Toggle(_haveDuration.boolValue);
            EditorGUILayout.EndHorizontal();

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
                AddField(_scale, level, width);
                if (_haveDuration.boolValue == true)
                {
                    AddField(_duration, level, width);
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
                SerializedProperty propertyName = property.FindPropertyRelative("Name");
                EditorGUILayout.LabelField(propertyName.stringValue, GUILayout.Width(width));
                GUILayout.Space(10);
            }

            if (_abilityProperties.arraySize < 5)
            {
                _propertyNameAdd = EditorGUILayout.TextField(_propertyNameAdd, GUILayout.Width(width));
                if (GUILayout.Button("+", GUILayout.Width(25)))
                {
                    _abilityProperties.arraySize += 1;
                    SerializedProperty property = _abilityProperties.GetArrayElementAtIndex(_abilityProperties.arraySize - 1);
                    SerializedProperty propertyName = property.FindPropertyRelative("Name");
                    SerializedProperty propertyValues = property.FindPropertyRelative("Values");
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
                    SerializedProperty propertyValues = property.FindPropertyRelative("Values");
                    if (propertyValues.arraySize < 2)
                    {
                        propertyValues.arraySize = 2;
                    }
                    AddField(propertyValues, level, width);
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
                    SerializedProperty propertyName = property.FindPropertyRelative("Name");

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

    protected int ChangeArrayWithButtons(int size, int width, bool canRemove)
    {
        EditorGUILayout.BeginHorizontal(GUILayout.Width(width));

        if (GUILayout.Button("+"))
        {
            size++;
        }

        if (canRemove)
        {
            if (GUILayout.Button("-"))
            {
                size--;
            }
        }

        EditorGUILayout.EndHorizontal();

        return size;
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

    protected void AddField(SerializedProperty array, int i, int width)
    {
        if (i < array.arraySize)
        {
            SerializedProperty element = array.GetArrayElementAtIndex(i);
            element.floatValue = EditorGUILayout.FloatField(element.floatValue, GUILayout.Width(width));
        }
        else if (i == _maxLevel + 1)
        {
            array.arraySize = ChangeArrayWithButtons(array.arraySize, width, array.arraySize > 2);
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
