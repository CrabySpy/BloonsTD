using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float speed = 1f;
    private Transform[] waypoints;
    private int waypointIndex = 0;

    public void SetPath(Transform[] path)
    {
        waypoints = path;
        transform.position = waypoints[0].position;
    }

    void Update()
    {
        if (waypoints == null || waypointIndex >= waypoints.Length)
            return;

        Transform target = waypoints[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                Destroy(gameObject); // balloons will disappear when they reach the end
            }
        }
    }
}