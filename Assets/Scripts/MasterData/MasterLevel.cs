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
    public MasterMovableRange PlayerMovableRange;
    public SquareModel playerInitialSquare;
    public List<MasterEnemy> enemies;
    public List<SquareModel> enemyInitialSquares;
    public List<SquareModel> enemyInitialRotation;

    public BoardMedel BoardMedel()
    {
        return new BoardMedel(boardSize);
    }

    public PlayerModel PlayerModel()
    {
        return new PlayerModel(PlayerMovableRange, playerInitialSquare);
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
