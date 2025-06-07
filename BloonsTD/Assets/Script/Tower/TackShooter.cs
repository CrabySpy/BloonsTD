using UnityEngine;
using UnityEditor;

public class TackShooter : Tower
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
    
    }
}
