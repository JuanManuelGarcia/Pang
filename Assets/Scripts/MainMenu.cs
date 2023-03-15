using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PointsSingleton.Instance.ResetPoints();        
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("Level_UI", LoadSceneMode.Single);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
