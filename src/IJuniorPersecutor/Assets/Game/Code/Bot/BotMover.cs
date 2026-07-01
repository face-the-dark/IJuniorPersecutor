using UnityEngine;

namespace Game.Code.Bot
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BotStepMover))]
    public class BotMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _minDistanceToPlayer = 3;
        [SerializeField] private BotDirectionCalculator _directionCalculator;
        [SerializeField] private Transform _player;

        private Transform _transform;
        private Rigidbody _rigidbody;
        private BotStepMover _stepMover;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _stepMover = GetComponent<BotStepMover>();
        }

        private void FixedUpdate() => 
            MoveToPlayer();

        private void MoveToPlayer()
        {
            Vector3 directionToPlayer =
                _directionCalculator.CalculateDirectionToPlayer(_transform.position, _player.position);
            
            float distanceToPlayer = (_player.position - _transform.position).sqrMagnitude;

            if (distanceToPlayer > _minDistanceToPlayer)
                _rigidbody.velocity = new Vector3(directionToPlayer.x * _moveSpeed, _rigidbody.velocity.y,
                    directionToPlayer.z * _moveSpeed);
            else
                _rigidbody.velocity = Vector3.zero;
            
            _stepMover.StepClimb(directionToPlayer);
        }
    }
}