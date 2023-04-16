using UnityEngine;

namespace Platformer.Enemies {
    public class EnemyHealth : MonoBehaviour {

        [SerializeField, ReadOnly] private bool m_isDead = false;

        private Collider2D m_boxCollider;
        private Rigidbody2D m_rigidbody;

        private void Awake() {
            m_boxCollider = GetComponent<BoxCollider2D> ();
            m_rigidbody = GetComponent<Rigidbody2D> ();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (m_isDead) return;

            if (other.gameObject.CompareTag("Player")) {
                var normal = other.contacts[0].normal;
                if (normal.y < -0.707) Die();
            }
        }

        private void Die() {
            m_isDead = true;
            m_boxCollider.enabled = false;

            m_rigidbody.velocity = new Vector2(2f, 10f);
            m_rigidbody.constraints = RigidbodyConstraints2D.None;
            m_rigidbody.angularVelocity = -70f;
        }
    }
}