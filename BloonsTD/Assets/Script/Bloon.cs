using UnityEngine;

public class Bloon : MonoBehaviour {
    public float speed = 1f;
    public Transform[] path;
    public GameObject weakerBloonPrefab;
    public int Health = 1;
    private int waypointIndex = 0;
    public bool isClone = false;

    void Update() {
        Move();
    }

    void Move() {
        if (waypointIndex >= path.Length) {
            ReachEnd();
            return;
        }

        Transform target = path[waypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f) {
            waypointIndex++;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void ReachEnd()
    {
        ShopScript shop = FindFirstObjectByType<ShopScript>();
        if (shop != null) {
            shop.LivesVar -= 20;
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damage) {
        if (!isClone) return;

        Health -= damage;

        if (Health <= 0) {
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

            if (newBloonScript != null) {
                newBloonScript.SetPath(path, waypointIndex);
                newBloonScript.speed = speed;
                newBloonScript.isClone = true;
            }
        }

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && audio.clip != null) {
            audio.Play();
            Destroy(gameObject, audio.clip.length);
        }
        else {
            Destroy(gameObject);
        }
    }
}
