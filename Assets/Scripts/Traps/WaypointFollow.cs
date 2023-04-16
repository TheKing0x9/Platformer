using UnityEngine;
using Pixelplacement;
using Platformer.Managers;

namespace Platformer.Traps {
    public class WaypointFollow : MonoBehaviour {
        [SerializeField] protected float _speed = 2f;
        [SerializeField] protected float _waitTime = 0.1f;
        [SerializeField] protected bool _loop = false;
        [SerializeField] protected bool _animate = true;

        [Space]
        [SerializeField] protected WaypointManager _waypoints;
        [SerializeField] protected int _offset;

        protected int _currentIndex = 0;
        protected int _direction = 1;

        private Animator m_animator;
        private int onParamID;

        private void Awake() {
            m_animator = GetComponent<Animator>();
            onParamID = Animator.StringToHash("On");
        }

        private void Start() {
            if (_offset < _waypoints.Count) _currentIndex = _offset;

            Vector3 position = _waypoints[_currentIndex].transform.position;;
            position.z = transform.position.z;
            transform.position = position;

            MoveToNextWaypoint();
        }

        protected virtual void MoveToNextWaypoint() {
            _currentIndex = IncrementIndex();
            Transform target = _waypoints[_currentIndex].transform;

            Vector3 to = target.position;
            to.z = transform.position.z;

            if (_animate) m_animator.SetBool(onParamID, false);

            float duration = (transform.position - to).magnitude / _speed;
            Tween.Position(transform, to, duration, _waitTime, Tween.EaseLinear, Tween.LoopType.None, OnTweenStart, MoveToNextWaypoint);
        }

        protected void OnTweenStart() {
            if (_animate) m_animator.SetBool(onParamID, true);
        }

        protected virtual int IncrementIndex() {
            int newIndex = _currentIndex + _direction;

            if (newIndex > _waypoints.Count - 1) {
                if (_loop) newIndex = 0;
                else {
                    newIndex--;
                    _direction *= -1;
                }
            }

            if (newIndex < 0) {
                _direction *= -1;
                newIndex= 0;
            }

            return newIndex;
        }
    }   
}