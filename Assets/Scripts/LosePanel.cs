using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] Text recordText;
    [SerializeField] private int coinsCount;
    public static bool proceed = false;

    private void Start()
    {
        proceed = false;
        int lastRunScore = PlayerPrefs.GetInt("lastRunScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");
        coinsCount = PlayerPrefs.GetInt("coins");

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

    public void Continue()
    {
        if (coinsCount >= 50)
        {
            proceed = true;
            coinsCount -= 50;
            PlayerPrefs.SetInt("coins", coinsCount);
            Time.timeScale = 1;
        }
    }
}