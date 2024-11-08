using UnityEngine;

namespace GAME.Scripts.Guards
{
    public class AttackStateData : IStateData
    {
        public Vector3 TargetPosition { get; private set; }
        
        public AttackStateData(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
        }
    }
}