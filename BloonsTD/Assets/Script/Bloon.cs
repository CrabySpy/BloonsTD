using UnityEngine;

public class Bloon : MonoBehaviour
{
    public float speed = 1f;
    public Transform[] path;
    public GameObject weakerBloonPrefab;
    public int Health = 1;

    private int waypointIndex = 0;

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex >= path.Length) return;

        Transform target = path[waypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            waypointIndex++;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Pop();
        }
    }

    public void SetPath(Transform[] waypoints)
    {
        path = waypoints;
    }

    void Pop()
    {
        // Spawn weaker bloon, if one exists
        if (weakerBloonPrefab != null)
        {
            GameObject newBloon = Instantiate(weakerBloonPrefab, transform.position, Quaternion.identity);
            Bloon newBloonScript = newBloon.GetComponent<Bloon>();

            if (newBloonScript != null)
            {
                newBloonScript.SetPath(path);
            }
        }

        // Play pop sound if assigned
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && audio.clip != null)
        {
            audio.Play();
            Destroy(gameObject, audio.clip.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
