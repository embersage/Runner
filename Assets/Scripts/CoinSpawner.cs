using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] CoinsPrefabs;
    [SerializeField] private Transform[] CoinsPositions;

    IEnumerator GenerateCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GameObject newCoinPrefab = Instantiate(CoinsPrefabs[0], CoinsPositions[Random.Range(0, 3)].position, Quaternion.identity);
            Destroy(newCoinPrefab, 5);
        }
    }

    private void Start()
    {
        StartCoroutine(GenerateCoin());
    }
}