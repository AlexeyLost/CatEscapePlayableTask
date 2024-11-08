using System.Collections.Generic;
using GAME.Scripts.Guards;
using UnityEngine;

namespace GAME.Scripts.Level 
{
    public class LevelView : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerSpawnPointTransform  { get; private set; }
        [field: SerializeField] public List<GuardBehaviourData> GuardsBehaviurDatas  { get; private set; }
        [field: SerializeField] public Transform GuardsParentTransform  { get; private set; }
    }
}
