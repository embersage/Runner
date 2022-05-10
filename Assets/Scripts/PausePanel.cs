using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    GameObject pausePanel;

    private void Start()
    {
        pausePanel = GameObject.Find("PausePanel");
    }

    public void Stop()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
