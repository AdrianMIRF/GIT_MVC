using UnityEditor;
using UnityEditor.UI;
using AdrianMunteanTest;

[CustomEditor(typeof(SelectOptionButton))]
public class SelectOptionButtonEditor : ButtonEditor
{
    SerializedProperty objectProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        objectProperty = serializedObject.FindProperty("SelectedImageRef");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        serializedObject.Update();

        EditorGUILayout.PropertyField(objectProperty);
        serializedObject.ApplyModifiedProperties();
    }
}