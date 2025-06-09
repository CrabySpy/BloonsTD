using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bloon balloon = other.GetComponent<Bloon>();
        if (balloon != null)
        {
            balloon.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
