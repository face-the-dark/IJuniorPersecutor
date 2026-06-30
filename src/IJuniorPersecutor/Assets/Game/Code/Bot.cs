using UnityEngine;

namespace Game.Code
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bot : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _minDistanceToPlayer = 3;
        [SerializeField] private Transform _player;
        [SerializeField] private float _inclinationRaycastMaxDistance = 2f;
        [SerializeField] private float _groundRaycastMaxDistance = 1.1f;
        [SerializeField] private LayerMask _inclinationLayer;
        [SerializeField] private LayerMask _groundLayer;

        private Transform _transform;
        private Rigidbody _rigidbody;

        private Vector3 _directionToPlayer;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            CalculateDirectionByPlane();
        }

        private void FixedUpdate()
        {
            MoveToPlayer();
        }

        private void CalculateDirectionByPlane()
        {
            _directionToPlayer = (_player.position - _transform.position).normalized;

            if (Physics.Raycast(transform.position, Vector3.down, _groundRaycastMaxDistance, _groundLayer))
                _directionToPlayer = Vector3.ProjectOnPlane(_directionToPlayer, Vector3.up);

            if (Physics.Raycast(_transform.position, _directionToPlayer, out RaycastHit hit, _inclinationRaycastMaxDistance, _inclinationLayer))
                _directionToPlayer = Vector3.ProjectOnPlane(_directionToPlayer, hit.normal);
        }

        private void MoveToPlayer()
        {
            float distanceToPlayer = (_player.position - _transform.position).sqrMagnitude;

            if (distanceToPlayer > _minDistanceToPlayer)
                _rigidbody.velocity = new Vector3
                (
                    _directionToPlayer.x * _moveSpeed,
                    _rigidbody.velocity.y,
                    _directionToPlayer.z * _moveSpeed
                );
            else
                _rigidbody.velocity = Vector3.zero;
        }
    }
}