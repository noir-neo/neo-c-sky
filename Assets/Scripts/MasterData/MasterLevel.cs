using System.Collections.Generic;
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

    public List<EnemyModel> EnemyModels()
    {
        var enemyModels = new List<EnemyModel>();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyModels.Add(new EnemyModel(enemies[i], enemyInitialSquares[i], enemyInitialRotation[i]));
        }
        return enemyModels;
    }

    void OnValidate()
    {
        enemyInitialSquares.StretchLength(enemies.Count);
        enemyInitialRotation.StretchLength(enemies.Count);
    }
}
