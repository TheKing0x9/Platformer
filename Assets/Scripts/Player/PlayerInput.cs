using UnityEngine;

namespace Platformer.Player {
    [DefaultExecutionOrder(-100)]
    public class PlayerInput : MonoBehaviour {
        [ReadOnly] public float horizontal;
        [ReadOnly] public bool jumpPressed;
        [ReadOnly] public bool jumpHeld;

        private bool m_readyToClear;
        [SerializeField, ReadOnly] private bool m_readInputs = true;

        private void Update() {
            if(!m_readInputs) return;

            ClearInput();

            ProcessInputs();

            horizontal = Mathf.Clamp(horizontal, -1f, 1f);
        }

        private void FixedUpdate() {
            m_readyToClear = true;
        }

        private void ClearInput() {
            if (!m_readyToClear) return;

            horizontal = 0f;
            jumpPressed = false;
            jumpHeld = false;

            m_readyToClear = false;
        }

        private void ProcessInputs() {
            horizontal += Input.GetAxis("Horizontal");

            jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
            jumpHeld = jumpHeld || Input.GetButton("Jump");
        }

        public void DisableInput() {
            m_readyToClear = true;
            ClearInput();

            m_readInputs = false;
        }

        public void EnableInput() {
            m_readInputs = true;
        }
    }
}