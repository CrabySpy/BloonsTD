using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class BloonGroup
    {
        public GameObject bloonPrefab;
        public int count;
        public float delayBetween = 0.5f;
    }

    [System.Serializable]
    public class Wave
    {
        public BloonGroup[] bloonGroups;
    }

    public Transform spawnPoint;
    public Transform[] path;
    public Wave[] waves;
    public float delayBetweenWaves = 3f;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;
    private ShopScript shopUI;

    public void StartNextWave(ShopScript caller)
    {
        if (!isSpawning && currentWaveIndex < waves.Length)
        {
            shopUI = caller;
            shopUI.IncrementRound();
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;
        }
        else if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
        }
    }
    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        foreach (BloonGroup group in wave.bloonGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                GameObject bloon = Instantiate(group.bloonPrefab, spawnPoint.position, Quaternion.identity);
                Bloon bloonScript = bloon.GetComponent<Bloon>();

                if (bloonScript != null)
                {
                    bloonScript.SetPath(path, 0);
                    bloonScript.speed = group.bloonPrefab.GetComponent<Bloon>().speed;
                    bloonScript.isClone = true;
                }

                yield return new WaitForSeconds(group.delayBetween);
            }
        }

        isSpawning = false;

        if (shopUI != null)
        {
            shopUI.OnWaveComplete(); // re-enable the start round button
        }
    }
}
