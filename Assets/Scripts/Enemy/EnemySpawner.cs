using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyThang enemyToSpawn;
    MasterLevel masterLevel;
    [SerializeField] float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        masterLevel = FindObjectOfType<MasterLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemy()
    {
        Instantiate(enemyToSpawn, transform);
        yield return new WaitForSeconds(Random.Range(spawnRate, (float)(spawnRate * 1.5)));
        if(!masterLevel.CheckForLevelComplete())
            StartCoroutine(SpawnEnemy());
    }
}
