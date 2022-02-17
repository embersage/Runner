using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Text scoreText;

    public float value = 0;
    public float speed = 3;

    private void Update()
    {
        value += Time.deltaTime * speed;
        scoreText.text = ((int)value).ToString();
    }

    //private void Update()
    //{
    //    for (int i = 0; i<=10000; i++)
    //        scoreText.text = ((int)i).ToString();

    //    //Debug.Log(player.position.z);
    //}
}
