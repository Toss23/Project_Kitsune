using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CustomEditor(typeof(SkinChanger))]
public class SkinChangerEditor : Editor
{
    private SerializedProperty _spriteLibraryAsset;
    private SerializedProperty _selectedCategory;
    private SerializedProperty _spriteResolvers;

    private SkinChanger _skinChanger;

    private void OnEnable()
    {
        _skinChanger = target as SkinChanger;

        _spriteLibraryAsset = serializedObject.FindProperty("_spriteLibraryAsset");
        _selectedCategory = serializedObject.FindProperty("_selectedCategory");
        _spriteResolvers = serializedObject.FindProperty("_spriteResolvers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sprite Library", GUILayout.Width(100));
        _spriteLibraryAsset.objectReferenceValue = EditorGUILayout.ObjectField(_spriteLibraryAsset.objectReferenceValue, typeof(SpriteLibraryAsset), false, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        
        if (_skinChanger.SpriteLibraryAsset != null) 
        {
            List<string> categories = new List<string>();
            categories.AddRange(_skinChanger.SpriteLibraryAsset.GetCategoryNames());

            if (categories.Count > 0)
            {
                int index = 0;

                for (int i = 0; i < categories.Count; i++)
                {
                    if (categories[i] == _selectedCategory.stringValue)
                    {
                        index = i;
                        break;
                    }
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Category", GUILayout.Width(100));
                index = EditorGUILayout.Popup(index, categories.ToArray(), GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();

                _selectedCategory.stringValue = categories[index];
            }
        }

        EditorGUILayout.PropertyField(_spriteResolvers);

        serializedObject.ApplyModifiedProperties();
    }
}