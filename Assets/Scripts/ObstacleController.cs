using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public static float speed = 30f;

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(1f);
        if (speed < 60)
        {
            speed += 0.25f;
            StartCoroutine(SpeedIncrease());
        }
    }

    void Start()
    {
        StartCoroutine(SpeedIncrease());
    }

    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }
}