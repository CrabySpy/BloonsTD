using UnityEngine;

public class PathHolder : MonoBehaviour
{
    public Transform[] waypoints; // these are waypoints that define the balloon's path (put them in order!!)

    public int Count => waypoints.Length;

    public Transform GetWaypoint(int index)
    {
        return waypoints[index];
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = Color.green; // visual path that'll show where the balloons will travel. delete this after everything has been set up kthx
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
