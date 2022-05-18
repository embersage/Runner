using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    GameObject pausePanel;
    public AudioSource buttonSound;

    private void Start()
    {
        //buttonSound.Play();
        pausePanel = GameObject.Find("PausePanel");
    }

    public void Stop()
    {
        buttonSound.Play();
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ToMenu()
    {
        buttonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        buttonSound.Play();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
