using UnityEngine;
using UnityEditor;

public class DartMonkey : Tower
{
    public GameObject projectilePrefab;
    public int numberOfProjectiles = 8;
    public float projectileSpeed = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Attack();
        }
    }


    public override void Attack()
    {
        float angleStep = 360f / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }
        }
    }
}
