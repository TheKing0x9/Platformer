using UnityEngine;
using System.Collections;

using Platformer.Player;
using Platformer.Managers;
using Platformer.Audio;

namespace Platformer.Items {
    public class Checkpoint : MonoBehaviour {

        private Animator m_animator;
        private int openParamID;

        private void Awake() {
            openParamID = Animator.StringToHash("Open");

            m_animator = GetComponent<Animator> ();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Player")) return;
            
            PlayerInput input = other.gameObject.GetComponent<PlayerInput> ();
            Rigidbody2D rigidbody = other.gameObject.GetComponent<Rigidbody2D> ();

            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
            input.DisableInput();

            m_animator.SetTrigger(openParamID);
            StartCoroutine(LevelCompletedTimer());

            AudioManager.instance.PlayVictorySound();
        }

        private IEnumerator LevelCompletedTimer() {
            yield return new WaitForSeconds(2f);
            GameManager.instance.LevelCompleted();
        }
    }
}