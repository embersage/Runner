using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float speed;

    void Start()
    {
        speed = 30f;
        StartCoroutine(SpeedIncrease());
    }

    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }

    private IEnumerator SpeedIncrease()
    {
        if (Time.timeScale != 10f)
        {
            yield return new WaitForSeconds(3);
            Time.timeScale += 0.05f;
            StartCoroutine(SpeedIncrease());
        }
        else
            StopCoroutine(SpeedIncrease());
    }
}
