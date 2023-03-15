using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndGameState : IGameState
{
    GameManager gameManager;
    bool unloaded = false;

    public GameEndGameState(GameManager gameManager)
    {
        PointsSingleton.Instance.AddPoints(100000);
        PointsSingleton.Instance.AddPoints(Mathf.CeilToInt(gameManager.TimeLeft) * 10000);

        this.gameManager = gameManager;
        gameManager.SetGameEndPanelActive(true);
        var lvlLoadingOp = SceneManager.UnloadSceneAsync(gameManager.CurrentLevel);
        lvlLoadingOp.completed += (dontcare) =>
        {
            unloaded = true;
        };
    }

    public void Do()
    {
        if (Input.GetButtonDown("Submit") && unloaded)
        {
            gameManager.SetGameEndPanelActive(false);
            gameManager.ClearObservers();
            SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
        }
    }
}
