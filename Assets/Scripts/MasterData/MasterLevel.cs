using System.Collections.Generic;
using System.Linq;
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

    public Dictionary<EnemyModel, PieceMover> EnemyModelMovers()
    {
        var enemyModelMovers = new Dictionary<EnemyModel, PieceMover>();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyModelMovers.Add(new EnemyModel(enemies[i], enemyInitialSquares[i], enemyInitialRotation[i]),
                enemies[i].PieceMover());
        }
        return enemyModelMovers;
    }

    public List<EnemyModel> EnemyModels()
    {
        var enemyModels = new List<EnemyModel>();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyModels.Add(new EnemyModel(enemies[i], enemyInitialSquares[i], enemyInitialRotation[i]));
        }
        return enemyModels;
    }

    public List<PieceMover> EnemyMovers()
    {
        return enemies.Select(e => e.InstantiatePiece())
            .Select(g => g.GetComponent<PieceMover>())
            .ToList();
    }

    void OnValidate()
    {
        enemyInitialSquares.StretchLength(enemies.Count);
        enemyInitialRotation.StretchLength(enemies.Count);
    }
}
