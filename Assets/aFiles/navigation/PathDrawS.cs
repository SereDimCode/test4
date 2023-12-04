using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathDrawS : MonoBehaviour
{
    public Transform[] waypoints;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(waypoints[i].position, waypoints[i + 1].position, NavMesh.AllAreas, path);

            for (int j = 0; j < path.corners.Length - 1; j++)
            {
                Gizmos.DrawLine(path.corners[j], path.corners[j + 1]);
            }
        }
    }
}
