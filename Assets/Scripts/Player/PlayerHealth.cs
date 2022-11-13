using UnityEngine;

using Platformer.Managers;

namespace Platformer.Player {
    public class PlayerHealth : MonoBehaviour {
        [SerializeField] private LayerMask m_trapsLayer;
        [SerializeField] private GameObject m_deathEffect;
        [SerializeField, ReadOnly] private bool m_isDead = false;
        public bool isDead { get => m_isDead; }

        private PlayerMovement m_movement;
        private PlayerInput m_input;
        private Collider2D m_collider;
        private Rigidbody2D m_rigidbody;

        private void Awake() {
            m_movement = GetComponent<PlayerMovement> ();
            m_collider = GetComponent<Collider2D> ();
            m_rigidbody = GetComponent<Rigidbody2D> ();
            m_input = GetComponent<PlayerInput> ();
        }

        private void OnTriggerEnter2D(Collider2D other) {            
            CheckDeadCollision(other);
            CheckDeadZCollision(other);
        }

        private void CheckDeadZCollision(Collider2D other) {
            if (other.gameObject.CompareTag("DeadZ")) {
                gameObject.SetActive(false);
                GameManager.instance.PlayerDied();
            }
        }

        private void CheckDeadCollision(Collider2D other) {
            int otherLayerMask = 1 << other.gameObject.layer;
            if ((otherLayerMask != m_trapsLayer) || m_isDead) return;
            
            m_isDead = true;

            m_movement.enabled = false;
            m_collider.isTrigger = true;

            float direction = m_input.horizontal;
            m_rigidbody.velocity = new Vector2(2f * direction, 10f);
            m_rigidbody.constraints = RigidbodyConstraints2D.None;
            m_rigidbody.angularVelocity = 70f * -direction;
        }
    }
}