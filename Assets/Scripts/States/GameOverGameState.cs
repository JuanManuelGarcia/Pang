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
            gameManager.SetGameOverPanelActive(false);
            PointsSingleton.Instance.ResetPoints();
            gameManager.State = new LevelLoadingGameState(gameManager);
        }
    }
}
