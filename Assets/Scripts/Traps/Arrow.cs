using UnityEngine;

using Platformer.Player;
using Platformer.Pickups;

namespace Platformer.Traps {
    public class Arrow : BasePickup {
        [SerializeField] private float m_jumpForce = 1.75f;
        
        protected override void OnPickupCollected(GameObject player) {
            Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D> ();
            PlayerMovement movement = player.GetComponent<PlayerMovement> ();

            if (movement && rigidbody) {
                rigidbody.velocity = Vector2.zero;
                movement.Jump(Vector2.up * m_jumpForce, true);
            }
        }
    }
}