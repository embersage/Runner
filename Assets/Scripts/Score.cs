using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    float totalScore;
    public float scoreMultiplier;
    [SerializeField] private Text scoreText;

    private void FixedUpdate()
    {
        
        totalScore += scoreMultiplier;
        scoreText.text = Mathf.Round(totalScore).ToString();
    }
}