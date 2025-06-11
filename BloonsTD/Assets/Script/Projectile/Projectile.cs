using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public Vector3 spawnPosition;

    protected virtual void Start()
    {
        spawnPosition = transform.position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Bloon bloon = other.GetComponent<Bloon>();
        if (bloon != null)
        {
            bloon.TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log("Projectile hit a bloon: " + bloon.name);
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
