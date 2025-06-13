using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 15f;

    private float timer = 3;
    private List<PowerUpCrate> activePowerUps = new List<PowerUpCrate>();

    void Update()
    {
        if (powerUpPrefab == null)
        {
            return;
        }

        timer += Time.deltaTime;
        CleanUpDestroyedCrates();

        if (timer >= spawnInterval)
        {
            SpawnCrates();
            timer = 0f;
        }
    }


    void SpawnCrates()
    {
        if (powerUpPrefab == null)
        {
            Debug.LogError("powerUpPrefab is not assigned in the Inspector.");
            return;
        }

        int count = 1;
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);
            GameObject obj = Instantiate(powerUpPrefab, pos, Quaternion.identity);
            PowerUpCrate crate = obj.GetComponent<PowerUpCrate>();
            activePowerUps.Add(crate);
        }

        CleanUpDestroyedCrates();  // <-- kills the crates after searching em

        SelectionSortPowerUps();

        PowerUpCrate best = FindBestPowerUp();
        if (best != null)
        {
            Debug.Log("coordinates: " + best.transform.position);
        }
    }

    void CleanUpDestroyedCrates()
    {
        activePowerUps.RemoveAll(crate => crate == null);
    }

    void SelectionSortPowerUps()
    {
        for (int i = 0; i < activePowerUps.Count - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < activePowerUps.Count; j++)
            {
                float dist1 = Vector3.Distance(activePowerUps[j].transform.position, Vector3.zero);
                float dist2 = Vector3.Distance(activePowerUps[minIndex].transform.position, Vector3.zero);

                if (dist1 < dist2 || (Mathf.Approximately(dist1, dist2) && activePowerUps[j].lifetime > activePowerUps[minIndex].lifetime))
                {
                    minIndex = j;
                }
            }

            // Swap!!!!!!!!!
            var temp = activePowerUps[i];
            activePowerUps[i] = activePowerUps[minIndex];
            activePowerUps[minIndex] = temp;
        }
    }

    PowerUpCrate FindBestPowerUp() // Lines 87 to 100 are chatgpt
    {
        foreach (PowerUpCrate crate in activePowerUps)
        {
            if (crate == null) continue;

            float dist = Vector3.Distance(crate.transform.position, Vector3.zero);
            if (crate.lifetime > 3f && dist < 4f)
            {
                return crate;
            }
        }
        return null;
    }
}