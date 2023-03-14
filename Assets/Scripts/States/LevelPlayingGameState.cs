using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelPlayingGameState : IGameState, IPlayerObserver
{
    GameManager gameManager;
    bool playerDied = false;

    public LevelPlayingGameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        var players = Object.FindObjectsOfType<MonoBehaviour>().OfType<IPlayer>();
        foreach (IPlayer s in players)
        {
            s.Attach(this);
        }
    }

    public void OnUpdate()
    {
        if(playerDied)
        {
            gameManager.State = new GameOverGameState(gameManager);
        }

        //if (Input.anyKeyDown)
        //{
        //    gameManager.State = new LevelFinishedGameState(gameManager);
        //}
    }

    public void PlayerDied(IPlayer subject)
    {
        playerDied = true;
    }
}
