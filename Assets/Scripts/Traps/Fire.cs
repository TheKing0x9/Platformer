using UnityEngine;

namespace Platformer.Traps {
    public class Fire : MonoBehaviour {
        [SerializeField] private float m_onDelay = 0.37f;
        [SerializeField] private float m_onDuration = 0.94f;
        [SerializeField] private Collider2D m_fireTrigger;

        [Header("Debug")]
        [SerializeField, ReadOnly] private bool m_on = false;

        private Animator m_animator;

        private int m_onParamID;
        private int m_hitParamID;

        private void Awake() {
            m_onParamID = Animator.StringToHash("On");
            m_hitParamID = Animator.StringToHash("Hit");

            m_animator = GetComponent<Animator> ();
        }

        private void Start() => Off();

        private void OnCollisionStay2D(Collision2D other) {
            bool isPlayer = other.gameObject.CompareTag("Player");
            bool isAbove =  other.transform.position.y > transform.position.y;

            if(isPlayer && isAbove && !m_on) {
                m_animator.SetTrigger(m_hitParamID);

                m_on = true;
                Invoke("On", m_onDelay);
            }
        }

        private void On() {
            m_animator.SetBool(m_onParamID, true);
            m_fireTrigger.enabled = true;

            Invoke("Off", m_onDuration);
        }

        private void Off() {
            m_on = false;

            m_animator.SetBool(m_onParamID, false);
            m_fireTrigger.enabled = false;
        }
    }
}