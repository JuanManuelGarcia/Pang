using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameEndPanel;
    [SerializeField] GameObject GameOverPanel;

    [SerializeField] string[] Levels;
    int currentLevel = 0;
    IGameState state;
    IPlayer currentPlayer;

    void Start()
    {
        state = new LevelLoadingGameState(this);
    }

    void Update()
    {
        state.OnUpdate();
    }

    public string CurrentLevel { get { return Levels[currentLevel]; } }

    public IGameState State { set { state = value; } }

    public void SetGameOverPanelActive(bool active)
    {
        GameOverPanel.SetActive(active);
    }

    public void SetGameEndPanelActive(bool active)
    {
        GameEndPanel.SetActive(active);
    }

    public bool IsThereANextLevel()
    {
        return currentLevel < Levels.Length - 1;
    }

    public void NextLevel()
    {
        currentLevel++;
    }
}
