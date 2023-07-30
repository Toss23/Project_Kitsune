using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CustomEditor(typeof(Skin¿ccessory))]
[CanEditMultipleObjects]
public class SkinAccessoryEditor : Editor
{
    private SerializedProperty _spriteLibraryAsset;
    private SerializedProperty _selectedCategory;

    private Skin¿ccessory _skin¿ccessory;

    private void OnEnable()
    {
        _skin¿ccessory = target as Skin¿ccessory;

        _spriteLibraryAsset = serializedObject.FindProperty("_spriteLibraryAsset");
        _selectedCategory = serializedObject.FindProperty("_selectedCategory");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sprite Library", GUILayout.Width(100));
        _spriteLibraryAsset.objectReferenceValue = EditorGUILayout.ObjectField(_spriteLibraryAsset.objectReferenceValue, typeof(SpriteLibraryAsset), false, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        if (_skin¿ccessory.SpriteLibraryAsset != null)
        {
            List<string> categories = new List<string>();
            categories.AddRange(_skin¿ccessory.SpriteLibraryAsset.GetCategoryNames());

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

        serializedObject.ApplyModifiedProperties();
    }
}