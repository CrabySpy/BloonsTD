using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float speed = 1f;
    public Transform[] path;
    private int waypointIndex = 0;

    [SerializeField] private int health = 1;

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
    health -= damage;

    if (health <= 0)
    {
        Pop(); // or Destroy(gameObject);
    }
}

    public void SetPath(Transform[] waypoints)
    {
        path = waypoints;
    }

    void Pop()
    {
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
