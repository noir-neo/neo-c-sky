using UnityEditor;
using UnityEngine;
using NeoC.Game.Board;

[CustomEditor(typeof(Board))]
[CanEditMultipleObjects]
public class BoardEditor : Editor
{
    private Board board;

    void OnEnable()
    {
        board = target as Board;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        string text = "SerializeSquaresInChildren";
        if(GUILayout.Button(text))
        {
            Undo.RecordObject(board, text);
            board.SerializeSquaresInChildren();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
