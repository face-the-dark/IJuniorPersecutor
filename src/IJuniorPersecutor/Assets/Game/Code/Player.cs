using UnityEngine;

namespace Game.Code
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private bool _isSimpleMove = true;

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            float directionValueByAxisX = Input.GetAxis(Horizontal);
            float directionValueByAxisZ = Input.GetAxis(Vertical);

            Vector3 moveDirection = new Vector3(directionValueByAxisX, 0f, directionValueByAxisZ);
            Vector3 moveVelocity = moveDirection * _moveSpeed;

            if (_isSimpleMove)
            {
                SimpleMove(moveVelocity);
            }
            else
            {
                Move(moveVelocity);
            }
        }

        private void SimpleMove(Vector3 moveVelocity)
        {
            _characterController.SimpleMove(moveVelocity);
        }

        private void Move(Vector3 moveVelocity)
        {
            moveVelocity += Physics.gravity;
            _characterController.Move(moveVelocity * Time.deltaTime);
        }
    }
}