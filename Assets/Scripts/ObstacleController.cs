using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] static float speed = 30f;

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(1);
        if (speed < 100) {
            speed += 0.1f;
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