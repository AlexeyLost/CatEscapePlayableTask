using System.Collections.Generic;
using UnityEngine;

namespace GAME.Scripts.Guards
{
    /// <summary>
    /// Helper to configurate guards when designing levels, without reference to guards views
    /// </summary>
    public class GuardBehaviourData : MonoBehaviour
    {
        [field: SerializeField] public GuardStateType StartStateType { get; private set; }
        [field: SerializeField] public List<Transform> PatrolPointsTransforms { get; private set; }
        [field: SerializeField] public Transform PointForEndlessMove { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public bool UsePlayerDetector { get; private set; }
        [field: SerializeField] public float MinDistanceToTarget { get; private set; }
    }
}