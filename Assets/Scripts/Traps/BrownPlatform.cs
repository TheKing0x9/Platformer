using UnityEngine;

namespace Platformer.Traps {
    public class BrownPlatform : WaypointFollow {
        [SerializeField, ReadOnly] protected bool _canMove = false;

        protected void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                _direction = 1;

                TryMove();
            }
        }

        protected void OnCollisionExit2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                _direction = -1;
                TryMove();
            }
        }

        protected override int IncrementIndex() {
            int newIndex = _currentIndex + _direction;

            if (newIndex > _waypoints.Count - 1) {
                _canMove = false;
                newIndex = _currentIndex;
            }

            if (newIndex < 0) {
                _canMove = false;
                newIndex = _currentIndex;
            }

            return newIndex;
        }

        private void TryMove() {
            if (_canMove)return;
            
            _canMove = true;
            MoveToNextWaypoint();
        }

        protected override void MoveToNextWaypoint() {
            if (!_canMove) return;

            base.MoveToNextWaypoint();
        }
    }
}