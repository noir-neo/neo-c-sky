using System;
using System.Collections.Generic;
using NeoC.Game;
using NeoC.Game.Model;
using UnityEngine;
using Taple = UniRx.Tuple;

[CreateAssetMenu]
public class MasterLevel : ScriptableObject
{
    public SquareModel boardSize;
    public SquareModel goalSquare;
    public MasterOccupiedRange PlayerOccupiedRange;
    public SquareModel playerInitialSquare;
    public List<MasterEnemy> enemies;
    public List<SquareModel> enemyInitialSquares;
    public List<SquareModel> enemyInitialRotation;

    public BoardMedel BoardMedel(Func<SquareModel, Vector3> getSquarePositionFunc)
    {
        return new BoardMedel(boardSize, goalSquare);
    }

    public PlayerModel PlayerModel()
    {
        return new PlayerModel(playerInitialSquare, PlayerOccupiedRange);
    }

    public IReadOnlyDictionary<EnemyModel, PieceMover> EnemyModelMovers(Func<SquareModel, Vector3> getSquarePositionFunc)
    {
        var enemyModelMovers = new Dictionary<EnemyModel, PieceMover>();
        for (int i = 0; i < enemies.Count; i++)
        {
            var model = new EnemyModel(enemies[i], enemyInitialSquares[i], enemyInitialRotation[i]);
            var position = getSquarePositionFunc(enemyInitialSquares[i]);
            var gameObject = enemies[i].InstantiatePiece(position, enemyInitialRotation[i].LookRotation());
            var mover = gameObject.GetComponent<PieceMover>();
            enemyModelMovers.Add(model, mover);
        }
        return enemyModelMovers;
    }

    void OnValidate()
    {
        enemyInitialSquares.StretchLength(enemies.Count);
        enemyInitialRotation.StretchLength(enemies.Count);
    }
}
