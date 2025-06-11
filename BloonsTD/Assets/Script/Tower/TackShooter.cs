using UnityEngine;
using UnityEditor;

public class TackShooter : Tower
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    
    protected override void RotateTowardsTarget(Bloon target) { }
    
    public override void Attack()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing projectilePrefab or firePoint on " + gameObject.name);
            return;
        }

        int numberOfNails = 8;
        float angleStep = 360f / numberOfNails;

        for (int i = 0; i < numberOfNails; i++)
        {
            float angle = angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.right;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation * Quaternion.Euler(0, 0, 180f));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction.normalized * projectileSpeed;
            }
        }
    }
}