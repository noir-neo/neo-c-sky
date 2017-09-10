using System;
using System.Collections.Generic;
using System.Linq;
using NeoC.Game;
using NeoC.Game.Model;
using UnityEngine;

[CreateAssetMenu]
public class MasterLevel : ScriptableObject
{
    public SquareModel boardSize;
    public SquareModel goalSquare;
    public MasterOccupiedRange PlayerOccupiedRange;
    public SquareModel playerInitialSquare;
    public InitialEnemy[] enemies;

    public BoardMedel BoardModel()
    {
        return new BoardMedel(boardSize, goalSquare);
    }

    public PlayerModel PlayerModel()
    {
        return new PlayerModel(playerInitialSquare, PlayerOccupiedRange);
    }

    public IEnumerable<Tuple<EnemyModel, PieceMover>> EnemyModels()
    {
        return enemies.Select(x => new Tuple<EnemyModel, PieceMover>(
            new EnemyModel(x.master, x.square, x.rotation),
            x.master.piecePrefab
            ));
    }

    [Serializable]
    public class InitialEnemy
    {
        public MasterEnemy master;
        public SquareModel square;
        public SquareModel rotation;
    }
}