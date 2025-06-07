using UnityEngine;

public class PathHolder : MonoBehaviour
{
    public Transform[] waypoints;

    public int Count => waypoints.Length;

    public Transform GetWaypoint(int index)
    {
        return waypoints[index];
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
