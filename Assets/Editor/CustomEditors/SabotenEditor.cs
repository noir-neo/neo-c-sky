using UnityEditor;
using UnityEngine;
using NeoC.Game;

[CustomEditor(typeof(Saboten))]
[CanEditMultipleObjects]
public class SabotenEditor : Editor
{
    private Saboten saboten;

    void OnEnable()
    {
        saboten = target as Saboten;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        if(GUILayout.Button("Serialize Children Rigidbodies"))
        {
            Undo.RecordObject(saboten, "Serialize Children Rigidbodies");
            saboten.SerializeChildrenRigidbodies();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
