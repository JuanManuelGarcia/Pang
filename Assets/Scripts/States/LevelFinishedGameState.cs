using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishedGameState : IGameState
{
    public LevelFinishedGameState(GameManager gameManager)
    {
        PointsSingleton.Instance.AddPoints(10000);
        PointsSingleton.Instance.AddPoints(Mathf.CeilToInt(gameManager.TimeLeft) * 10000);

        var lvlLoadingOp = SceneManager.UnloadSceneAsync(gameManager.CurrentLevel);
        lvlLoadingOp.completed += (dontcare) =>
        {
            lvlLoadingOp = null;
            gameManager.NextLevel();
            gameManager.State = new LevelLoadingGameState(gameManager);
        };
    }

    public void Do()
    {

    }
}
