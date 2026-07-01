using UnityEngine;

namespace Game.Code.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private bool _isSimpleMove = true;

        private CharacterController _characterController;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable() => 
            _playerInput.Moved += OnMoved;

        private void OnDisable() => 
            _playerInput.Moved -= OnMoved;

        private void OnMoved(Vector3 moveDirection)
        {
            Vector3 moveVelocity = moveDirection * _moveSpeed;

            if (_isSimpleMove)
                SimpleMove(moveVelocity);
            else
                Move(moveVelocity);
        }

        private void SimpleMove(Vector3 moveVelocity) => 
            _characterController.SimpleMove(moveVelocity);

        private void Move(Vector3 moveVelocity) => 
            _characterController.Move((moveVelocity + Physics.gravity) * Time.deltaTime);
    }
}