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
        if (other.CompareTag("PowerUp"))
        {
            PowerUpCrate crate = other.GetComponent<PowerUpCrate>();
            if (crate != null)
            {
                crate.ActivatePowerUp();
            }

            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Bloon"))
        {
            Bloon bloon = other.GetComponent<Bloon>();
            if (bloon != null)
            {
                bloon.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
