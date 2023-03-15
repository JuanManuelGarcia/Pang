using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelPlayingGameState : IGameState, IPlayerObserver
{
    GameManager gameManager;
    bool playerDied = false;

    public LevelPlayingGameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        var players = Object.FindObjectsOfType<MonoBehaviour>().OfType<IPlayerSubject>();
        foreach (IPlayerSubject s in players)
        {
            s.Attach(this);
        }
    }

    public void Do()
    {
        if(playerDied)
        {
            gameManager.State = new GameOverGameState(gameManager);
        }

        //TODO: to state LevelFinished (Observe balls)
        //if (Input.anyKeyDown)
        //{
        //    gameManager.State = new LevelFinishedGameState(gameManager);
        //}
    }

    public void Update(ISubject subject)
    {
        if (subject is IPlayerSubject)
            playerDied = (subject as IPlayerSubject).IsDead;
    }
}
