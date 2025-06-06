using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] bloonTypes;     // List of bloon prefabs
        public int[] bloonCounts;           // Matching count for each bloon
        public float spawnDelay = 0.5f;     // Time between spawns
    }

    public PathHolder path;
    public Wave[] waves;
    public Transform spawnPoint;
    public int currentWave = 0;

    private bool isSpawning = false;

    void Update()
    {
        if (!isSpawning && Input.GetKeyDown(KeyCode.Space)) // Start next wave on Space
        {
            if (currentWave < waves.Length)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
                currentWave++;
            }
            else
            {
                Debug.Log("All waves completed!");
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        for (int i = 0; i < wave.bloonTypes.Length; i++)
        {
            for (int j = 0; j < wave.bloonCounts[i]; j++)
            {
                GameObject bloon = Instantiate(wave.bloonTypes[i], spawnPoint.position, Quaternion.identity);
                bloon.GetComponent<Balloon>().SetPath(path.waypoints);
                yield return new WaitForSeconds(wave.spawnDelay);
            }
        }

        isSpawning = false;
    }
}
