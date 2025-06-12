using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] private float explosionRadius = 0.4f;
    [SerializeField] private int explosionDamage = 1;
    [SerializeField] private LayerMask bloonLayer;
    [SerializeField] private GameObject explosionEffectPrefab;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Explode();
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, bloonLayer);
        foreach (var hit in hits)
        {
            Bloon bloon = hit.GetComponent<Bloon>();
            if (bloon != null)
            {
                bloon.TakeDamage(explosionDamage);
            }
        }

        if (explosionEffectPrefab != null)
        {
            GameObject effect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.23f);
        }
    

        Destroy(gameObject);
    }
}
