using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bloon bloon = other.GetComponent<Bloon>();
        if (bloon != null)
        {
            bloon.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
