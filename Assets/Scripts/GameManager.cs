using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ITimerSubject
{
    [Serializable]
    public struct Level
    {
        public string SceneName;
        public float TimeInSeconds;
    }

    [SerializeField] GameObject GameEndPanel;
    [SerializeField] GameObject GameOverPanel;

    [SerializeField] Level[] Levels;

    public string CurrentLevel { get { return Levels[currentLevel].SceneName; } }
    public float CurrentLevelTime { get { return Levels[currentLevel].TimeInSeconds; } }

    public IGameState State { set { state = value; } }
    public float TimeLeft { get; set; }

    List<IObserver> observers = new List<IObserver>();
    int currentLevel = 0;
    IGameState state;

    void Start()
    {
        state = new LevelLoadingGameState(this);
    }

    void Update()
    {
        state.Do();

        if (Input.GetButtonDown("Cancel"))
        {
            SetGameEndPanelActive(false);
            ClearObservers();
            SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
        }
    }

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

    public void ClearObservers()
    {
        observers.Clear();
    }

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        if (observers.Contains(observer)) observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver o in observers) o.Revise(this);
    }
}
