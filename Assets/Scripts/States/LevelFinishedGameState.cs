using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishedGameState : IGameState
{
    public LevelFinishedGameState(GameManager gameManager)
    {
        if (gameManager.IsThereANextLevel())
        {
            var scene = SceneManager.GetSceneByName(gameManager.CurrentLevel);
            var lvlLoadingOp = SceneManager.UnloadSceneAsync(scene);
            lvlLoadingOp.completed += (dontcare) =>
            {
                lvlLoadingOp = null;
                gameManager.NextLevel();
                gameManager.State = new LevelLoadingGameState(gameManager);
            };
        }
        else gameManager.State = new GameEndGameState(gameManager);
    }

    public void Do()
    {

    }
}
