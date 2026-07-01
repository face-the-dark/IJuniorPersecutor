using System;
using UnityEngine;

namespace Game.Code.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        
        public event Action<Vector3> Moved;
        
        private void Update()
        {
            float directionValueByAxisX = Input.GetAxis(Horizontal);
            float directionValueByAxisZ = Input.GetAxis(Vertical);

            Vector3 moveDirection = new Vector3(directionValueByAxisX, 0f, directionValueByAxisZ);
            
            Moved?.Invoke(moveDirection);
        }
    }
}