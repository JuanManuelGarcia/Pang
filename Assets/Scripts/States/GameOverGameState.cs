using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverGameState : IGameState
{
    GameManager gameManager;
    bool unloaded = false;

    public GameOverGameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.SetGameOverPanelActive(true);

        var scene = SceneManager.GetSceneByName(gameManager.CurrentLevel);
        var lvlLoadingOp = SceneManager.UnloadSceneAsync(scene);
        lvlLoadingOp.completed += (dontcare) =>
        {
            unloaded = true;
        };
    }

    public void OnUpdate()
    {
        if (Input.anyKeyDown && unloaded)
        {
            gameManager.SetGameOverPanelActive(false);
            gameManager.State = new LevelLoadingGameState(gameManager);
        }
    }
}
