using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float speed;

    void Start()
    {
        speed = 30f;
    }

    void FixedUpdate()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }
}
