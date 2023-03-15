using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelPlayingGameState : IGameState, IPlayerObserver, IBallObserver
{
    GameManager gameManager;
    bool playerDied = false;
    int numBalls;

    public LevelPlayingGameState(GameManager gameManager)
    {
        this.gameManager = gameManager;

        var players = Object.FindObjectsOfType<MonoBehaviour>().OfType<IPlayerSubject>();
        foreach (IPlayerSubject s in players)
        {
            s.Attach(this);
        }

        numBalls = 0;
        var balls = Object.FindObjectsOfType<MonoBehaviour>().OfType<IBallSubject>();
        foreach (IBallSubject s in balls)
        {
            numBalls++;
            s.Attach(this);
        }
    }

    public void Do()
    {
        if(playerDied)
        {
            gameManager.State = new GameOverGameState(gameManager);
        }

        if (numBalls == 0)
        {
            gameManager.State = new LevelFinishedGameState(gameManager);
        }
    }

    public void Update(ISubject subject)
    {
        if (subject is IPlayerSubject)
        {
            playerDied = (subject as IPlayerSubject).IsDead; // No need to detach, player clears observers after death
        }

        if (subject is IBallSubject)
        {
            foreach(ISubject s in (subject as IBallSubject).BallsSpawned)
            {
                numBalls++;
                s.Attach(this);
            }
            numBalls--;  // No need to detach, ball clears observers after death
        }
    }
}
