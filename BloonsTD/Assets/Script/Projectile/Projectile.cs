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
        other.GetComponent<PowerUpCrate>().ActivatePowerUp();
        Destroy(gameObject);
    }

    if (other.CompareTag("Bloon"))
    {
        other.GetComponent<Bloon>().TakeDamage(1);
        Destroy(gameObject);
    }

}


    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
