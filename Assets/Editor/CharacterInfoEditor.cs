using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterInfo), true)]
public class CharacterInfoEditor : Editor
{
    private SerializedProperty _prefab;

    private SerializedProperty _life; 
    private SerializedProperty _regeneration;
    private SerializedProperty _damage;
    private SerializedProperty _critChance;
    private SerializedProperty _critMultiplier; 
    private SerializedProperty _armour;

    private SerializedProperty _abilities;

    private void OnEnable()
    {
        _prefab = serializedObject.FindProperty("_prefab");

        _life = serializedObject.FindProperty("_life");
        _regeneration = serializedObject.FindProperty("_regeneration");
        _damage = serializedObject.FindProperty("_damage");
        _critChance = serializedObject.FindProperty("_critChance");
        _critMultiplier = serializedObject.FindProperty("_critMultiplier");
        _armour = serializedObject.FindProperty("_armour");

        _abilities = serializedObject.FindProperty("_abilities");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIStyle boldStyle = new GUIStyle();
        boldStyle.normal.textColor = Color.white;
        boldStyle.fontStyle = FontStyle.Bold;

        Texture2D prefabTexture = null;
        if (_prefab.objectReferenceValue != null)
        {
            GameObject prefab = (GameObject)_prefab.objectReferenceValue;
            if (prefab != null)
                prefabTexture = AssetPreview.GetAssetPreview(prefab);
        }

        EditorGUILayout.LabelField("Main", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prefab", GUILayout.Width(60));
        _prefab.objectReferenceValue = EditorGUILayout.ObjectField(_prefab.objectReferenceValue, typeof(GameObject), false, GUILayout.Width(170));
        EditorGUILayout.EndHorizontal();

        int size = 200;
        if (_prefab.objectReferenceValue != null & prefabTexture != null)
        {
            GUILayout.Space(5);
            EditorGUILayout.LabelField("", GUILayout.Width(size), GUILayout.Height(size));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), prefabTexture);
        }
        else
        {
            Color baseColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.black;

            GUIStyle previewStyle = new GUIStyle();
            previewStyle.normal.textColor = Color.white;
            previewStyle.fontStyle = FontStyle.Bold;
            previewStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("Preview", previewStyle, GUILayout.Width(size), GUILayout.Height(size));
            EditorGUILayout.EndHorizontal();

            GUI.backgroundColor = baseColor;
        }

        GUILayout.Space(10);

        int width = 100;
        int space = 5;
        EditorGUILayout.LabelField("States", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Life", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Regeneration", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Armour", GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        _life.floatValue = EditorGUILayout.FloatField(_life.floatValue, GUILayout.Width(width));
        GUILayout.Space(space);
        _regeneration.floatValue = EditorGUILayout.FloatField(_regeneration.floatValue, GUILayout.Width(width));
        GUILayout.Space(space);
        _armour.floatValue = EditorGUILayout.FloatField(_armour.floatValue, GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Crit Chance", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Crit Multiplier", GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        _damage.floatValue = EditorGUILayout.FloatField(_damage.floatValue, GUILayout.Width(width));
        GUILayout.Space(space);
        _critChance.floatValue = EditorGUILayout.FloatField(_critChance.floatValue, GUILayout.Width(width));
        GUILayout.Space(space);
        _critMultiplier.floatValue = EditorGUILayout.FloatField(_critMultiplier.floatValue, GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10);

        if (_abilities.arraySize != 5)
            _abilities.arraySize = 5;

        SerializedProperty[] ability = new SerializedProperty[_abilities.arraySize];
        bool[] haveAbility = new bool[_abilities.arraySize];
        for (int i = 0; i < _abilities.arraySize; i++)
        {
            ability[i] = _abilities.GetArrayElementAtIndex(i);
            haveAbility[i] = ability[i].objectReferenceValue != null;
        }

        width = 150;
        EditorGUILayout.LabelField("Abilities", boldStyle);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Attack", GUILayout.Width(width));
        GUILayout.Space(space);
        EditorGUILayout.LabelField("Ultimate", GUILayout.Width(width));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        ability[0].objectReferenceValue = EditorGUILayout.ObjectField(ability[0].objectReferenceValue, typeof(Ability), false, GUILayout.Width(width), GUILayout.Height(30));
        GUILayout.Space(space);
        ability[4].objectReferenceValue = EditorGUILayout.ObjectField(ability[4].objectReferenceValue, typeof(Ability), false, GUILayout.Width(width), GUILayout.Height(30));
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
            ability[i].objectReferenceValue = EditorGUILayout.ObjectField(ability[i].objectReferenceValue, typeof(Ability), false, GUILayout.Width(width), GUILayout.Height(30));
            GUILayout.Space(space);
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}