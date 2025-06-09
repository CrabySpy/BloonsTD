using UnityEngine;

public class BloonSpawner : MonoBehaviour
{
    public PathHolder pathHolder;
    public GameObject redBloonPrefab;
    public GameObject blueBloonPrefab;
    public GameObject greenBloonPrefab;
    public GameObject yellowBloonPrefab;

    void Start()
    {
        StartCoroutine(SpawnBloons());
    }

    System.Collections.IEnumerator SpawnBloons()
    {
        yield return new WaitForSeconds(1f); // wait a moment before spawning

        SpawnBloon(redBloonPrefab, 1f);     // slowest
        yield return new WaitForSeconds(1f);
        SpawnBloon(blueBloonPrefab, 1.5f);
        yield return new WaitForSeconds(1f);
        SpawnBloon(greenBloonPrefab, 2f);
        yield return new WaitForSeconds(1f);
        SpawnBloon(yellowBloonPrefab, 2.5f); // fastest
    }

    void SpawnBloon(GameObject prefab, float speed)
    {
        GameObject bloon = Instantiate(prefab);
        Bloon balloonScript = bloon.GetComponent<Bloon>();
        balloonScript.speed = speed;
        balloonScript.SetPath(pathHolder.waypoints);
    }
}
