using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int totalScore;
    public int scoreMultiplier;

    public float value = 0;
    public float speed = 3;

    private void Update()
    {
        //totalScore += scoreMultiplier;
        //scoreText.text = totalScore.ToString();
        value += Time.deltaTime * speed;
        scoreText.text = ((int)value).ToString();
    }
}
