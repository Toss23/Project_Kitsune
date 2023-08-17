using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RangeAbilityData))]
public class RangeAbilityDataEditor : BaseAbilityDataEditor
{
    private SerializedProperty _speed;
    private SerializedProperty _spawnOffset;

    private SerializedProperty _count;
    private SerializedProperty _tiltAngle;

    private SerializedProperty _aimNearestEnemy;
    private SerializedProperty _followNearestEnemy;
    private SerializedProperty _destroyOnHit;

    protected override void InitProperties()
    {
        base.InitProperties();

        //
        _speed = serializedObject.FindProperty("_speed");
        _spawnOffset = serializedObject.FindProperty("_spawnOffset");

        _count = serializedObject.FindProperty("_count");
        _tiltAngle = serializedObject.FindProperty("_tiltAngle");

        _aimNearestEnemy = serializedObject.FindProperty("_aimNearestEnemy");
        _followNearestEnemy = serializedObject.FindProperty("_followNearestEnemy");
        _destroyOnHit = serializedObject.FindProperty("_destroyOnHit");
    }

    protected override void AdditionalTabButton(int width)
    {
        base.AdditionalTabButton(width);

        //
        AddTabButton("Range", width);
    }

    protected override void AdditionalTab(int width)
    {
        base.AdditionalTab(width);

        //
        InitPropertyArray(_count, _tiltAngle);
        if (_openedTab == "Range")
        {
            AddFloatField(_speed, "Speed", width + 50);
            AddFloatField(_spawnOffset, "Spawn Offset", width + 50);
            AddBoolField(_aimNearestEnemy, "Aim Nearest Enemy", width + 50);
            AddBoolField(_followNearestEnemy, "Follow Nearest Enemy", width + 50);
            AddBoolField(_destroyOnHit, "Destroy on Hit", width + 50);

            GUILayout.Space(10);

            AddColumns(width, "Count", "Tilt Angle");
            for (int level = 1; level <= _maxLevel + 1; level++)
            {
                EditorGUILayout.BeginHorizontal();
                AddLevelField(level, width);
                AddField<int>(_count, level, 1, width);
                AddField<float>(_tiltAngle, level, 0, width);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
