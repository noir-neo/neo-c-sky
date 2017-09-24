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
        const string serializeSquaresInChildrenText = "SerializeSquaresInChildren";
        if(GUILayout.Button(serializeSquaresInChildrenText))
        {
            Undo.RecordObject(board, serializeSquaresInChildrenText);
            board.SerializeSquaresInChildren();
        }

        const string calculateBoardsCoordinateText = "CalculateBoardsCoordinate";
        if(GUILayout.Button(calculateBoardsCoordinateText))
        {
            Undo.RecordObject(board, calculateBoardsCoordinateText);
            board.CalculateBoardsCoordinate();
        }

        const string generateBoardFromMasterText = "GenerateBoardFromMaster";
        if(GUILayout.Button(generateBoardFromMasterText))
        {
            Undo.RecordObject(board, generateBoardFromMasterText);
            board.GenerateBoardFromMaster();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
