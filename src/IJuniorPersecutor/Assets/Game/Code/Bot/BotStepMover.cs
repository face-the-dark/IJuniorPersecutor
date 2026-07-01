using UnityEngine;

namespace Game.Code.Bot
{
    [RequireComponent(typeof(Rigidbody))]
    public class BotStepMover : MonoBehaviour
    {
        [SerializeField] private float _stepOffset = 0.3f;
        [SerializeField] private LayerMask _stepLayer;
        [SerializeField] private float _downPointOffset = 0.9f;
        [SerializeField] private float _stepRaycastMaxDistance = 0.5f;
        [SerializeField] private float _smoothFactor = 0.5f;

        private Rigidbody _rigidbody;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody>();

        public void StepClimb(Vector3 directionToPlayer)
        {
            Vector3 downPoint = transform.position - Vector3.up * _downPointOffset;
            Vector3 upPoint = downPoint + Vector3.up * _stepOffset;

            if (Physics.Raycast(downPoint, directionToPlayer, _stepRaycastMaxDistance, _stepLayer))
            {
                if (Physics.Raycast(upPoint, directionToPlayer, _stepRaycastMaxDistance, _stepLayer) == false)
                {
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.MovePosition(Vector3.Lerp(_rigidbody.position,
                        _rigidbody.position + Vector3.up * _stepOffset, _smoothFactor));
                }
            }
        }
    }
}