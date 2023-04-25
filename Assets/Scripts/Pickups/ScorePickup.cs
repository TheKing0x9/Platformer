using UnityEngine;

using Platformer.Managers;
using Platformer.Audio;

namespace Platformer.Pickups {
    public class ScorePickup : BasePickup {
        [SerializeField] private int m_score = 100;
        protected override void OnPickupCollected(GameObject player) {
            // Add Score to Game Manager
            GameManager.instance.AddScore(m_score);
            AudioManager.instance.PlayPickupSound();
        }
    }
}