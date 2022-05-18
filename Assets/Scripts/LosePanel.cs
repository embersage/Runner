using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] Text recordText;
    PlayerController player;
    GameObject continueButton;
    public AudioSource buttonSound;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        continueButton = GameObject.Find("Continue Button");

        int lastRunScore = PlayerPrefs.GetInt("lastRunScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");

        if (lastRunScore > recordScore)
        {
            recordScore = lastRunScore;
            PlayerPrefs.SetInt("recordScore", recordScore);
            recordText.text = recordScore.ToString();
        }
        else
        {
            recordText.text = recordScore.ToString();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartSound()
    {
        buttonSound.Play();
    }

    public void Continue()
    {
        buttonSound.Play();
        if (player.coinsCount >= 50)
        {
            player.losePanel.SetActive(false);
            Destroy(player.hitObstacle);
            player.coinsCount -= 50;
            PlayerPrefs.SetInt("coins", player.coinsCount);
            player.coinsText.text = player.coinsCount.ToString();
            Time.timeScale = 1;
        }
        else
            continueButton.SetActive(false);
    }

    public void ToMenu()
    {
        buttonSound.Play();
        SceneManager.LoadScene(0);
    }
}