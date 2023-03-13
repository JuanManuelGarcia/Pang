using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        LevelLoading,
        LevelPlaying,
        GameOver,
        LevelFinished,
        GameEnd
    }

    [SerializeField] GameObject GameEndPanel;
    [SerializeField] GameObject GameOverPanel;

    [SerializeField]
    string[] Levels =
    {
        "Level_001","Level_002","Level_003"
    };

    AsyncOperation lvlLoadingOp;
    int currentLevel = 0;
    GameState state = GameState.LevelLoading;


    void Update()
    {
        switch (state)
        {
            case GameState.LevelLoading:

                if (lvlLoadingOp == null)
                {
                    lvlLoadingOp = SceneManager.LoadSceneAsync(Levels[currentLevel], LoadSceneMode.Additive);
                    lvlLoadingOp.completed += (dontcare) => 
                    {
                        lvlLoadingOp = null;
                        state = GameState.LevelPlaying; 
                    };
                }

                break;

            case GameState.LevelPlaying:

                if (Input.anyKeyDown)
                {
                    state = GameState.LevelFinished;
                }

                break;

            case GameState.GameOver: //temp
                
                GameOverPanel.SetActive(true);
                if (Input.anyKeyDown)
                {
                    state = GameState.GameEnd;
                }

                break;


            case GameState.LevelFinished:

                if (currentLevel < Levels.Length - 1)
                {
                    if (lvlLoadingOp == null)
                    {
                        var scene = SceneManager.GetSceneByName(Levels[currentLevel]);
                        lvlLoadingOp = SceneManager.UnloadSceneAsync(scene);
                        lvlLoadingOp.completed += (dontcare) =>
                        {
                            state = GameState.LevelLoading;
                            lvlLoadingOp = null;
                            currentLevel++;
                        };
                    }
                }
                else state = GameState.GameEnd;

                break;

            case GameState.GameEnd:

                GameEndPanel.SetActive(true);
                if(Input.anyKeyDown)
                {
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                }

                break;
        }
    }
}
