using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private Ragdoll ragdoll;

    private void Update()
    {
        if (ragdoll.canSpawn == true)
        {
            Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.Euler(new Vector3(0,180,0)));
            ragdoll.canSpawn = false;
        }

        ragdoll = FindAnyObjectByType<Ragdoll>().GetComponent<Ragdoll>();
    }
}
