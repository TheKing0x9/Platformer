using UnityEngine;

using Platformer.Player;

namespace Platformer.Traps {
    public class Trampoline : MonoBehaviour {
        [SerializeField] private float m_throwForce = 13f;
        
        private int m_throwParamID;
        private Animator m_animator;
        private PlayerMovement m_player = null;

        private void Awake() {
            m_throwParamID = Animator.StringToHash("Throw");

            m_animator = GetComponent<Animator> ();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            bool isPlayer = other.gameObject.CompareTag("Player");
            bool isAbove =  other.transform.position.y > transform.position.y;

            if(isPlayer && isAbove) {
                PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement> ();

                if (movement) {
                    m_player = movement;
                    m_animator.SetTrigger(m_throwParamID);
                }
            }
        }

        private void Throw() {
            if (!m_player) return;

            m_player.Jump(Vector2.up * m_throwForce);
            m_player = null;
        }
    }
}