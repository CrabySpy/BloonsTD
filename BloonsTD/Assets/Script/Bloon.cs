using UnityEngine;

public class Bloon : MonoBehaviour
{
    public float speed = 1f;
    public Transform[] path;
    public GameObject weakerBloonPrefab;
    public int Health = 1;
    private int waypointIndex = 0;
    public bool isClone = false;
    public ShopScript ShopScript;

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

        // Clamp to Z = 0 so they don’t fall below map
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public void TakeDamage(int damage)
    {
        if (!isClone) return; // prevents balloons that are just sitting in the game (like the gameobjects) from being popped

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
        ShopScript.MoneyVar = ShopScript.MoneyVar + 1;
        ShopScript.UpdateRoundText();
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
