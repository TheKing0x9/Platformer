using Platformer.Managers;
using UnityEngine;

namespace Platformer.Utilities {
    public class Waypoint : MonoBehaviour {
        public Waypoint nextWaypoint = null;
        public bool isLooped = false;
        public WaypointManager waypointManager;

        private void OnDrawGizmos() {
            if (!waypointManager.visualizePath) return;

            if (nextWaypoint != null) {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, nextWaypoint.transform.position);
            } else {
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(transform.position, 0.35f);
            }
        }
        
        private void OnDestroy() {
            waypointManager.FixWaypointLinks(this);
            waypointManager.RemoveWaypoint(this);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.35f);
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);//for showing direction
        }

        
    }
}