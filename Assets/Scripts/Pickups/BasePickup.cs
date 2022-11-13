using UnityEngine;

namespace Platformer.Pickups {
    public abstract class BasePickup : MonoBehaviour {
        [SerializeField] private GameObject m_collectionEffect;

        protected void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                OnPickupCollected(other.gameObject);
                
                Destroy(gameObject);
                Instantiate(m_collectionEffect, transform.position, Quaternion.identity, null);
            }
        }

        protected abstract void OnPickupCollected(GameObject player);
    }
}