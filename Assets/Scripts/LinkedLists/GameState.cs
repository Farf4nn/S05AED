using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public Vector3 playerPosition;

    public int health;
    public int attack;

    public List<Vector3> enemyPositions = new List<Vector3>();

    public GameState Clone()
    {
        GameState copy = new GameState();

        copy.playerPosition = playerPosition;
        copy.health = health;
        copy.attack = attack;

        copy.enemyPositions = new List<Vector3>(enemyPositions);

        return copy;
    }
}
