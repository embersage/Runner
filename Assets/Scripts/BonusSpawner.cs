using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] BonusesPrefabs;
    [SerializeField] private Transform[] BonusesPositions;

    IEnumerator GenerateBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            GameObject newBonusPrefab = Instantiate(BonusesPrefabs[Random.Range(0, 2)], BonusesPositions[Random.Range(0, 3)].position, Quaternion.identity);
            Destroy(newBonusPrefab, 5);
        }
    }

    private void Start()
    {
        StartCoroutine(GenerateBonus());
    }
}
