using System;
using System.Collections.Generic;
using GAME.Scripts.Core;
using GAME.Scripts.Core.Services;
using GAME.Scripts.Guards;
using GAME.Scripts.Player;
using Object = UnityEngine.Object;

namespace GAME.Scripts.Level 
{
    /// <summary>
    /// Main level controller, that spawns level and other things.
    /// Also observes some events that can trigger game over.
    /// </summary>
    public class LevelController
    {
        public event Action<bool> GameOver;
        
        private LevelView _view;
        private PlayerController _playerController;
        private List<GuardController> _guards = new();
        private TickableService _tickableService;
        private GameMode _gameMode;

        public LevelController(GameMode gameMode)
        {
            _gameMode = gameMode;
            _tickableService = ServiceLocator.Instance.GetService<TickableService>();
        }
        

        public void SpawnLevel(LevelView levelViewPrefab, PlayerView playerViewPrefab, GuardView guardViewPrefab) 
        {
            _view = Object.Instantiate(levelViewPrefab);
            if (_gameMode == GameMode.Playable) SpawnPlayer(playerViewPrefab);
            SpawnGuards(guardViewPrefab);
        }

        public void StartGame()
        {
            _playerController?.AllowGameplay();
        }

        private void SpawnPlayer(PlayerView playerViewPrefab)
        {
            _playerController = new PlayerController(ServiceLocator.Instance.GetService<PlayerInputService>());
            _tickableService.AddFixedUpdateable(_playerController);
            _playerController.Initialize(playerViewPrefab, _view.PlayerSpawnPointTransform);
        }

        private void SpawnGuards(GuardView guardViewPrefab) 
        {
            foreach (GuardBehaviourData guardBehaviourData in _view.GuardsBehaviurDatas)
            {
                GuardController guardController = new GuardController(guardBehaviourData);
                guardController.Init(guardViewPrefab, _view.GuardsParentTransform);
                _tickableService.AddUpdateable(guardController);
                _tickableService.AddLateUpdateable(guardController);
                _guards.Add(guardController);

                guardController.PlayerDetected += OnPlayerDetected;
            }
        }

        private void OnPlayerDetected()
        {
            foreach (GuardController guard in _guards)
            {
                guard.PlayerDetected -= OnPlayerDetected;
            }
            
            _playerController.DisableMovement();
            GameOver?.Invoke(false);
        }
    }
}
