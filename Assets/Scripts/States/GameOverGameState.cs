using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverGameState : IGameState
{
    GameManager gameManager;

    public GameOverGameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.SetGameOverPanelActive(true);
    }

    public void OnUpdate()
    {
        if (Input.anyKeyDown)
        {
            gameManager.SetGameOverPanelActive(false);
            gameManager.State = new GameEndGameState(gameManager);
        }
    }
}
