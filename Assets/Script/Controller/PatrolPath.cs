using System;
using UnityEngine;

namespace Dreams.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        private const float waypointGizmosRadius = 0.5f;
        private void OnDrawGizmos()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmosRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 >= transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
