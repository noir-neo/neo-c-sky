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
        string serializeSquaresInChildrenText = "SerializeSquaresInChildren";
        if(GUILayout.Button(serializeSquaresInChildrenText))
        {
            Undo.RecordObject(board, serializeSquaresInChildrenText);
            board.SerializeSquaresInChildren();
        }

        string calculateBoardsCoordinateText = "CalculateBoardsCoordinate";
        if(GUILayout.Button(calculateBoardsCoordinateText))
        {
            Undo.RecordObject(board, calculateBoardsCoordinateText);
            board.CalculateBoardsCoordinate();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
