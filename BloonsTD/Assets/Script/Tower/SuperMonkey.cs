using UnityEngine;

public class SuperMonkey : Tower
{
    public GameObject ProjectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if (ProjectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing ProjectilePrefab or firePoint on " + gameObject.name);
            return;
        }

        GameObject projectile = Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 270f));

        
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * projectileSpeed; // Right assumes your dart faces right
        }
    }
}