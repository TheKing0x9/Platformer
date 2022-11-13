
using System.Collections;
using UnityEngine;

namespace Platformer.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Properties")]
        [SerializeField] private float m_speed = 8f;
        [SerializeField] private float m_jumpSpeed = 5f;
        [SerializeField] private float m_coyoteDuration = 0.05f;
        [SerializeField] private float m_maxFallSpeed = 25f;

        [Header("Jump Properties")]
        [SerializeField] private float m_jumpForce = 4.7f;
        [SerializeField] private float m_doubleJumpForce = 2.9f;
        [SerializeField] private float m_jumpHoldForce = 0.81f;
        [SerializeField] private float m_jumpHoldDuration = 0.1f;

        [Header("Wall Jump Properties")]
        [SerializeField] private Vector2 m_wallJumpVelocity = new Vector2(5f, 5.6f);
        [SerializeField] private float m_maxWallSlideSpeed = 10f;
        [SerializeField] private float m_wallJumpLerp = 10f;

        [Header("Environment Check Properties")]
        [SerializeField] private float m_footOffset = .4f;
        // [SerializeField] private float m_pivotOffset = 0.5f;
        [SerializeField] private float m_groundDistance = 0.1f;
        [SerializeField] private float m_eyeHeight = 0.4f;
        [SerializeField] private float m_grabDistance = 0.2f;
        [SerializeField] private LayerMask m_groundMask; 

        [Header("Debug")]
        [SerializeField] private bool m_drawDebugRaycasts = true;

        [Header("Status Check Properties")]
        [ReadOnly] public bool isOnGround;
        [ReadOnly] public bool isWallSliding;
        [ReadOnly] public bool isJumping;   
        [ReadOnly] public bool isDoubleJump;
        [ReadOnly] public bool wallJumped;

        private Rigidbody2D m_rigidbody;
        private PlayerInput m_input;

        private float m_jumpTime;
        private float m_coyoteTime;
        private float m_originalXScale;
        private int m_direction = 1;
        private bool m_canMove = true;

        // Start is called before the first frame update
        void Awake()
        {
            m_originalXScale = transform.localScale.x;
            
            m_rigidbody = GetComponent<Rigidbody2D> ();
            m_input = GetComponent<PlayerInput> ();
        }

        // Update is called once per frame
        void FixedUpdate() {
            StatusChecks();

            HorizontalMovement();
            VerticalMovement();
            ClampVelocity();
        }

        void StatusChecks() {
            isOnGround = false;
            isWallSliding = false;

            RaycastHit2D leftCheck = Raycast(new Vector2(-m_footOffset, 0f), Vector2.down, m_groundDistance);
            RaycastHit2D rightCheck = Raycast(new Vector2(m_footOffset, 0f), Vector2.down, m_groundDistance);

            if (leftCheck || rightCheck) {
                isOnGround = true;
                isDoubleJump = false;
                wallJumped = false;
            }

            RaycastHit2D wallCheck = Raycast(new Vector2(m_footOffset * m_direction, m_eyeHeight), Vector2.right * m_direction, m_grabDistance);

            if (wallCheck && !isOnGround &&  m_rigidbody.velocity.y < 0) {
                isWallSliding = true;
                isDoubleJump = false;
            }
        }

        void HorizontalMovement() {
            if (!m_canMove) return;

            float xVelocity = m_input.horizontal * (isOnGround ? m_speed : m_jumpSpeed);
            if (xVelocity * m_direction < 0) Flip();

            Vector2 movementVector = new Vector2(xVelocity, m_rigidbody.velocity.y);

            if (wallJumped) {
                m_rigidbody.velocity = Vector2.Lerp(m_rigidbody.velocity, movementVector, m_wallJumpLerp * Time.deltaTime);
            } else {
                m_rigidbody.velocity = movementVector;
            }

            if (isOnGround) 
                m_coyoteTime = Time.time + m_coyoteDuration;
        }

        void VerticalMovement() {
            if(isWallSliding) {
                if (!isOnGround && m_input.jumpPressed) WallJump();
                else WallSlide();
            } else if (!isJumping && m_input.jumpPressed && (isOnGround || Time.time < m_coyoteTime)) {
                isOnGround = false;
                isJumping = true;

                Jump(Vector2.up * m_jumpForce);
                m_jumpTime = Time.time + m_jumpHoldDuration;
            } else if (!isJumping && !isOnGround && !isDoubleJump && m_input.jumpPressed) {
                Jump(Vector2.up * m_doubleJumpForce);
                isDoubleJump = true;
            } else if (isJumping) {
                if (m_input.jumpHeld)
                    Jump(Vector2.up * m_jumpHoldForce, true);

                if (Time.time > m_jumpTime)
                    isJumping = false;
            }
        }

        public void Jump(Vector2 velocity, bool additive = false) {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, additive ? m_rigidbody.velocity.y : 0f );
            m_rigidbody.velocity += velocity;
        }

        private void ClampVelocity() {
            Vector2 vel = m_rigidbody.velocity;

            if (vel.y < -m_maxFallSpeed) 
                vel.y = m_maxFallSpeed;

            if (Mathf.Abs(vel.x) < 0.1f)
                vel.x = 0f;

            m_rigidbody.velocity = vel;
        }

        private void WallSlide() {
            if (!m_canMove) return;

            bool pushingWall = m_rigidbody.velocity.x * m_direction > 0;
            
            float push = pushingWall ? 0 : m_rigidbody.velocity.x;
            m_rigidbody.velocity = new Vector2(push, -m_maxWallSlideSpeed);
        }

        private void WallJump() {
            Flip();
            isWallSliding = false;

            StopCoroutine(DisableMovement(0f));
            StartCoroutine(DisableMovement(0.1f));
        
            Vector2 jumpVelocity = new Vector2(m_wallJumpVelocity.x * m_direction, m_wallJumpVelocity.y);

            Jump(jumpVelocity);
            wallJumped = true;
        }

        private IEnumerator DisableMovement(float duration) {
            m_canMove = false;
            yield return new WaitForSeconds(duration);
            m_canMove = true;
        }

        private void Flip() {
            m_direction *= -1;

            Vector3 localScale = transform.localScale;
            localScale.x = m_originalXScale * m_direction;
            transform.localScale = localScale;
        }

        private RaycastHit2D Raycast(Vector2 offset, Vector2 direction, float distance) {
            return Raycast(offset, direction, distance, m_groundMask);
        }

        private RaycastHit2D Raycast(Vector2 offset, Vector2 direction, float distance, LayerMask mask) {
            Vector2 pos = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(pos + offset, direction, distance, mask);

            if (m_drawDebugRaycasts) {
                Color color = hit ? Color.green : Color.red;
                Debug.DrawRay(pos + offset, direction * distance, color);
            }

            return hit;
        }
    }
}