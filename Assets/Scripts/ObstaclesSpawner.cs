using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] ObstaclesPrefabs;
    [SerializeField] private Transform[] ObstaclesPositions;

    private void Start()
    {
        StartCoroutine(GenerateObstacle());
    }

    IEnumerator GenerateObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject newEnemyPrefab = Instantiate(ObstaclesPrefabs[Random.Range(0, 2)], ObstaclesPositions[Random.Range(0, 3)].position, Quaternion.identity);
            Destroy(newEnemyPrefab, 5);
        }
    }
}