using System;
using UnityEngine;

namespace Game.Code.Bot
{
    [Serializable]
    public class BotDirectionCalculator
    {
        [SerializeField] private float _inclinationRaycastMaxDistance = 2f;
        [SerializeField] private float _groundRaycastMaxDistance = 1.1f;
        [SerializeField] private LayerMask _inclinationLayer;
        [SerializeField] private LayerMask _groundLayer;

        public Vector3 CalculateDirectionToPlayer(Vector3 botPosition, Vector3 playerPosition)
        {
            Vector3 directionToPlayer = (playerPosition - botPosition).normalized;

            if (Physics.Raycast(botPosition, Vector3.down, _groundRaycastMaxDistance, _groundLayer))
                directionToPlayer = Vector3.ProjectOnPlane(directionToPlayer, Vector3.up);

            if (Physics.Raycast(botPosition, directionToPlayer, out RaycastHit hit, _inclinationRaycastMaxDistance,
                    _inclinationLayer))
                directionToPlayer = Vector3.ProjectOnPlane(directionToPlayer, hit.normal);

            return directionToPlayer;
        }
    }
}