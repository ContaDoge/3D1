using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    [Header("Shared Waypoints")]
    public Transform[] waypoints; // Array of waypoints shared by all platforms

    private void OnDrawGizmos()
    {
        // Draw gizmos to visualize waypoints in the scene
        if (waypoints != null && waypoints.Length > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);

                if (i + 1 < waypoints.Length)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}
