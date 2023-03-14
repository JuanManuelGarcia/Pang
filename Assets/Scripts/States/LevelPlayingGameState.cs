using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayingGameState : IGameState
{
    GameManager gameManager;

    public LevelPlayingGameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void OnUpdate()
    {
        if (Input.anyKeyDown)
        {
            gameManager.State = new LevelFinishedGameState(gameManager);
        }
    }
}
