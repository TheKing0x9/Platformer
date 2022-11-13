using UnityEngine;
using Pixelplacement;
using Platformer.Managers;

namespace Platformer.Traps {
    public class WaypointFollow : MonoBehaviour {
        [SerializeField] private float m_speed = 2f;
        [SerializeField] private float m_waitTime = 0.1f;
        [SerializeField] private bool m_loop = false;

        [Space]
        [SerializeField] private WaypointManager m_waypoints;

        private int m_currentIndex = 0;
        private int m_direction = 1;

        private void Start() {
            Vector3 position = m_waypoints[m_currentIndex].transform.position;;
            position.z = transform.position.z;
            transform.position = position;

            MoveToNextWaypoint();
        }

        private void MoveToNextWaypoint() {
            m_currentIndex = IncrementIndex();
            Transform target = m_waypoints[m_currentIndex].transform;

            Vector3 to = target.localPosition;
            to.z = transform.position.z;

            float duration = (transform.position - to).magnitude / m_speed;
            Tween.Position(transform, to, duration, m_waitTime, Tween.EaseLinear, Tween.LoopType.None, null, MoveToNextWaypoint);
        }

        private int IncrementIndex() {
            int newIndex = m_currentIndex + m_direction;

            if (newIndex > m_waypoints.Count - 1) {
                if (m_loop) newIndex = 0;
                else {
                    newIndex--;
                    m_direction *= -1;
                }
            }

            if (newIndex < 0) {
                m_direction *= -1;
                newIndex= 0;
            }

            return newIndex;
        }
    }   
}