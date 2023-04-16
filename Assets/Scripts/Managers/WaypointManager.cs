using UnityEngine;
using System.Collections.Generic;

using Waypoint = Platformer.Utilities.Waypoint;

namespace Platformer.Managers {
    public class WaypointManager : MonoBehaviour {
        [SerializeField] private List<Waypoint> m_waypoints;

        [SerializeField] private bool m_visualizePath = true;
        public bool visualizePath { get => m_visualizePath; }

        public Waypoint this [int index] { get { return m_waypoints[index]; } }
        public int Count { get => m_waypoints.Count; }

        public Waypoint CreateWaypoint() {
            GameObject newWaypoint = new GameObject("Waypoint_" + m_waypoints.Count);
            Waypoint waypoint = newWaypoint.AddComponent<Waypoint> ();

            if (m_waypoints.Count > 0) {
                Waypoint prevWaypoint = m_waypoints[m_waypoints.Count - 1];

                prevWaypoint.nextWaypoint = waypoint;
                prevWaypoint.isLooped = false;

                InitWaypoint(waypoint, prevWaypoint.transform);
            } else {
                InitWaypoint(waypoint, transform);
            }

            m_waypoints.Add(waypoint);
            return waypoint;
        }

         public Waypoint InsertWaypoint(Waypoint waypoint) {
            GameObject newWaypoint = new GameObject();
            Waypoint w = newWaypoint.AddComponent<Waypoint> ();

            int current_Index = m_waypoints.IndexOf(waypoint);

            InitWaypoint(waypoint, waypoint.transform);

            m_waypoints.Insert(current_Index + 1, waypoint);

            w.nextWaypoint = waypoint.nextWaypoint;
            w.isLooped = waypoint.isLooped;
            waypoint.isLooped = false;
            waypoint.nextWaypoint = waypoint;

            UpdateWaypointNames();

            return w;
        }

        private void InitWaypoint(Waypoint w, Transform t) {
            w.transform.parent = transform;
            w.waypointManager = this;

            w.transform.position = t.position;
            w.transform.rotation = t.rotation;
            w.transform.position = w.transform.up * 0.82f;
        }

        public void UpdateWaypointNames() {
            for (int i = 0; i < m_waypoints.Count; i++) {
                m_waypoints[i].transform.name = "Waypoints_" + i;
            }
        }

        public void RemoveWaypoint(Waypoint toRemove) {
            m_waypoints.Remove(toRemove);
            UpdateWaypointNames();
        }
        
        public Waypoint GetInitialWaypoint() {
            if (m_waypoints.Count > 0)
                return m_waypoints[0];
            else
                return CreateWaypoint();
        }

        public void FixWaypointLinks(Waypoint waypoint) {
            int previous_waypointIndex = m_waypoints.IndexOf(waypoint) - 1;

            if (previous_waypointIndex >= 0)
                m_waypoints[previous_waypointIndex].nextWaypoint = waypoint.nextWaypoint;
        }

        public bool IsEmpty() {
            return m_waypoints.Count == 0;
        }

        public void ClearWaypoints() {
            for (int i = m_waypoints.Count - 1; i >= 0; --i)
                DestroyImmediate(m_waypoints[i].gameObject);

            m_waypoints.Clear();
        }

        private void OnDestroy() {
            ClearWaypoints();
        }

        public void DeleteWaypoint()
        {
            DestroyImmediate(m_waypoints[m_waypoints.Count - 1].gameObject);
        }
    }
}