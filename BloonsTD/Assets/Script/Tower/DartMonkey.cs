using UnityEngine;
using UnityEditor;

public class DartMonkey : Tower
{
    public GameObject dartPrefab;
    [SerializeField] private float projectileSpeed = 10f;


    public override void Attack()
    {
        if (dartPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing dartPrefab or firePoint on " + gameObject.name);
            return;
        }

        GameObject dart = Instantiate(dartPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 270f));

        
        Rigidbody2D rb = dart.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * projectileSpeed; // Right assumes your dart faces right
        }
    }
}

