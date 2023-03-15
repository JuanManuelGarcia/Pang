using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndGameState : IGameState
{
    GameManager gameManager;

    public GameEndGameState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        gameManager.SetGameEndPanelActive(true);
    }

    public void Do()
    {
        if (Input.GetButtonDown("Submit"))
        {
            gameManager.SetGameEndPanelActive(false);
            SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
        }
    }
}
