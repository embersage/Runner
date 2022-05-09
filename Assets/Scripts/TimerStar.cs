using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerStar : MonoBehaviour
{
    public float timeStart = 5;
    public Text textTimer;


    void Start()
    {
        textTimer.text = timeStart.ToString();
    }

    void Update()
    {
        timeStart -= Time.deltaTime;
        textTimer.text = Mathf.Round(timeStart).ToString();
        if (timeStart < 0) timeStart = 0;
    }
}
