using UnityEngine;

public class Bloon : MonoBehaviour
{
    public float speed = 1f;
    public Transform[] path;
    public GameObject weakerBloonPrefab;
    public int Health = 1;
    private int waypointIndex = 0;
    public bool isClone = false;

    public float Progress { get; private set; }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex >= path.Length) return;

        Transform target = path[waypointIndex];
        Vector3 prevPos = transform.position;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        float distanceMoved = Vector2.Distance(prevPos, transform.position);
        Progress += distanceMoved;

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            waypointIndex++;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BloonKiller"))
        {
            ShopScript shop = FindFirstObjectByType<ShopScript>();
            if (shop != null)
            {
                shop.LivesVar -= 1;
                shop.UpdateText();
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isClone) return;

        Health -= damage;

        if (Health <= 0)
        {
            Pop();
        }
    }

    public void SetPath(Transform[] waypoints, int startIndex = 0)
    {
        path = waypoints;
        waypointIndex = startIndex;
    }

    void Pop()
    {
        if (weakerBloonPrefab != null)
        {
            GameObject newBloon = Instantiate(weakerBloonPrefab, transform.position, Quaternion.identity);
            Bloon newBloonScript = newBloon.GetComponent<Bloon>();

            if (newBloonScript != null)
            {
                newBloonScript.SetPath(path, waypointIndex);
                newBloonScript.speed = speed;
                newBloonScript.isClone = true;
            }
        }

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
