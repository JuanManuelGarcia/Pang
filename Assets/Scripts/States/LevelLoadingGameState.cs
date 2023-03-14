using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadingGameState : IGameState
{
    public LevelLoadingGameState(GameManager gameManager)
    {
        var lvlLoadingOp = SceneManager.LoadSceneAsync(gameManager.CurrentLevel, LoadSceneMode.Additive);
        lvlLoadingOp.completed += (dontcare) => 
        {
            lvlLoadingOp = null;
            gameManager.State = new LevelPlayingGameState(gameManager); 
        };
    }

    public void OnUpdate()
    {
    }
}
