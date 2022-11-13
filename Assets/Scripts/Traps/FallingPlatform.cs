using UnityEngine;
using Pixelplacement;

namespace Platformer.Traps {
    public class FallingPlatform : MonoBehaviour {

        [Header("Durations")]
        [SerializeField] private float m_fallTime = 2f;
        [SerializeField] private float m_recoverTime = 4f;

        [Header("Tween")]
        [SerializeField] private float m_fallDistance = 1f;
        [SerializeField] private float m_fallTweenDuration = 1f;

        private Animator m_animator;
        private Collider2D m_collider;
        private SpriteRenderer m_renderer;

        private int m_onParamID;
        private Vector3 m_originalPosition;
        private Color m_transparent = new Color(1, 1, 1, 0);

        private void Awake() {
            m_originalPosition = transform.localPosition;
            m_onParamID = Animator.StringToHash("On");

            m_collider = GetComponent<Collider2D> ();
            m_animator = GetComponent<Animator> ();
            m_renderer = GetComponent<SpriteRenderer> ();
        }

        private void Start () => On();

        private void OnCollisionEnter2D(Collision2D other) {
            bool isPlayer = other.gameObject.CompareTag("Player");
            bool isAbove = other.transform.position.y > transform.position.y;

            if(isPlayer && isAbove) {
                Invoke("Off", m_fallTime);
            }
        }

        private void Off() {
            m_animator.SetBool(m_onParamID, false);
            m_collider.enabled = false;

            Vector3 targetPosition = transform.position;
            targetPosition.y -= m_fallDistance;

            Tween.Position(transform, targetPosition, m_fallTweenDuration, 0f, Tween.EaseOut);
            Tween.Color(m_renderer, m_transparent, m_fallTweenDuration, 0f, Tween.EaseOut, Tween.LoopType.None, null, OffTweenCompleted);
        }

        private void OffTweenCompleted() {
            m_renderer.enabled = false;
            Invoke("On", m_recoverTime);
        }

        private void On() {
            m_renderer.enabled = true;
            m_renderer.color = Color.white;

            transform.localPosition = m_originalPosition;

            m_animator.SetBool(m_onParamID, true);
            m_collider.enabled = true;
        }
    }
}