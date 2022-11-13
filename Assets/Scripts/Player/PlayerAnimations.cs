using System.Collections;
using UnityEngine;

namespace Platformer.Player {
    public class PlayerAnimations : MonoBehaviour {
        private PlayerMovement m_movement;
        private PlayerInput m_input;
        private PlayerHealth m_health;
        private Rigidbody2D m_rigidbody2D;
        private SpriteRenderer m_renderer;
        private Animator m_animator;

        private int speedParamID;
        private int groundParamID;
        private int fallParamID;
        private int doubleJumpParam;
        private int wallSlideParam;
        private int hitParamID;
        private int enterParamID;
        private int exitParamID;

        private bool m_isDead = false;

        private void Awake() {
            speedParamID = Animator.StringToHash("horizontal");
            groundParamID = Animator.StringToHash("isOnGround");
            fallParamID = Animator.StringToHash("verticalSpeed");
            doubleJumpParam = Animator.StringToHash("isDoubleJump");
            wallSlideParam = Animator.StringToHash("isWallSliding");
            hitParamID = Animator.StringToHash("hit");
            enterParamID = Animator.StringToHash("enter");
            exitParamID = Animator.StringToHash("exit");

            m_movement = transform.parent.GetComponent<PlayerMovement> ();
            m_input = transform.parent.GetComponent<PlayerInput> ();
            m_health = transform.parent.GetComponent<PlayerHealth>(); 
            m_rigidbody2D = transform.parent.GetComponent<Rigidbody2D> ();
            m_animator = GetComponent<Animator> ();
            m_renderer = GetComponent<SpriteRenderer> ();
        }

        private void Start() {
            StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation() {
            m_rigidbody2D.Sleep();
            m_renderer.enabled = false;
            m_input.DisableInput();

            yield return new WaitForSeconds(0.5f);

            m_renderer.enabled = true;
            m_animator.SetTrigger(enterParamID);
        }

        private void Update() {
            m_animator.SetFloat(speedParamID, Mathf.Abs(m_input.horizontal));
            m_animator.SetFloat(fallParamID, m_rigidbody2D.velocity.y);

            m_animator.SetBool(groundParamID, m_movement.isOnGround);
            m_animator.SetBool(wallSlideParam, m_movement.isWallSliding);
            m_animator.SetBool(doubleJumpParam, m_movement.isDoubleJump);

            if (m_isDead != m_health.isDead) {
                m_isDead = m_health.isDead;
                m_animator.SetTrigger(hitParamID);
            }
        }

        private void EntryAnimationDone() {
            m_input.EnableInput();
            m_rigidbody2D.WakeUp();
        }
    }
}