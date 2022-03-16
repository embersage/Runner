using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    float totalScore;
    public float scoreMultiplier;
    [SerializeField] public Text scoreText;

    private void FixedUpdate()
    {
        
        totalScore += scoreMultiplier;
        scoreText.text = Mathf.Round(totalScore).ToString();
    }
}