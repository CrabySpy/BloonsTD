using UnityEngine;

public class Nail : Projectile
{
    private float maxTravelDistance = 1f;

    private void Update()
    {
        float distanceTravelled = Vector3.Distance(spawnPosition, transform.position);
            if (distanceTravelled >= maxTravelDistance)
            {
                Destroy(gameObject);
                Debug.Log("Nail destroyed due to max travel distance reached.");
            }
    }

}

