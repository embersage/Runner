using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ObstaclesPrefabs;
    [SerializeField] private Transform[] ObstaclesPositions;

    IEnumerator GenerateObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject newObstaclePrefab = Instantiate(ObstaclesPrefabs[Random.Range(0, 2)], ObstaclesPositions[Random.Range(0, 3)].position, Quaternion.identity);
            Destroy(newObstaclePrefab, 5);
        }
    }

    private void Start()
    {
        StartCoroutine(GenerateObstacle());
    }
}