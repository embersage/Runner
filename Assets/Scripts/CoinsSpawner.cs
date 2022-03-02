using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] CoinsPrefabs;
    [SerializeField] private Transform[] CoinsPositions;

    private void Start()
    {
        StartCoroutine(GenerateCoin());
    }

    IEnumerator GenerateCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            GameObject newEnemyPrefab = Instantiate(CoinsPrefabs[Random.Range(0, 1)], CoinsPositions[Random.Range(0, 3)].position, Quaternion.identity);
            Destroy(newEnemyPrefab, 5);
        }
    }
}