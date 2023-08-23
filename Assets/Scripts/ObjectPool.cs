using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Size of the object pool (amount of enemies to spwan)")]
    int poolSize = 5;

    [SerializeField]
    [Tooltip("Enemy prefab to spawn")]
    GameObject enemyPrefab;

    [SerializeField]
    [Tooltip("Spawn timer")]
    float spawnTimer = 1f;

    GameObject[] pool;

	void Awake()
	{
        PopulatePool();
	}

    // Populate the object pool
	void PopulatePool()
	{
		pool = new GameObject[poolSize];

        // loop through the pool array
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
	}

	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Function for spawning enemy
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    // Function that enables game objects in pool
	void EnableObjectInPool()
	{
		for(int i = 0; i < pool.Length; i++)
        {
            // Checks if game object in the pool is active or not
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
	}
}
