using UnityEngine;
using System.Collections;

using Platformer.Extensions;

namespace Platformer.Traps {
    public class Fan : MonoBehaviour {
        [SerializeField] private float m_stateTime = 2f;
        [SerializeField] private bool m_on;

        private Animator m_animator;
        private Collider2D m_collider;

        private void Start() {
            m_animator = GetComponent<Animator> ();
            m_collider = GetComponent<Collider2D> ();

            StartCoroutine(Repeat());
        }

        IEnumerator Repeat() {
            while (true) {
                yield return new WaitForSeconds(m_stateTime);
                m_on = !m_on;
                m_collider.enabled = m_on;
                m_animator.SetBool("On", m_on);
            }
        }
    }
}