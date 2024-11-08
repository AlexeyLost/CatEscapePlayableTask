using GAME.Scripts.Guards;
using GAME.Scripts.Level;
using GAME.Scripts.Player;
using UnityEngine;

namespace GAME.Scripts.Core
{
    /// <summary>
    /// Just storage for prefabs, better to use asset references from addressables,
    /// but for playable i did't import addressables.
    /// </summary>
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "GAME/Prefabs")]
    public class PrefabsData : ScriptableObject
    {
        [field: SerializeField] public LevelView LevelViewPrefab { get; private set; }
        [field: SerializeField] public PlayerView PlayerViewPrefab { get; private set; }
        [field: SerializeField] public GuardView GuardViewPrefab { get; private set; }
    }
}