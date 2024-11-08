using GAME.Scripts.Core;
using GAME.Scripts.Core.Services;
using GAME.Scripts.Events;
using UnityEngine;

namespace GAME.Scripts.Player
{
    /// <summary>
    /// Player entity brain.
    /// </summary>
    public class PlayerController : IFixedUpdateable
    {
        private PlayerView _view;
        private PlayerInputService _inputService;
        private PlayerMovementController _movementController;
        private bool _canMove;

        public PlayerController(PlayerInputService inputService)
        {
            _inputService = inputService;
            _canMove = false;
        }

        public void Initialize(PlayerView playerViewPrefab, Transform playerSpawnPointTransform)
        {
            _view = Object.Instantiate(playerViewPrefab, playerSpawnPointTransform.position, Quaternion.identity);
            _view.transform.SetParent(playerSpawnPointTransform);
            _movementController = new PlayerMovementController(_inputService, _view);

            _view.ExitLevel += OnPlayerExitLevel;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            if (!_canMove) return;
            
            _movementController.UpdateMovement(fixedDeltaTime);
        }

        public void AllowGameplay()
        {
            _canMove = true;
        }

        public void DisableMovement()
        {
            _canMove = false;
            _movementController.StopMovement();
        }

        private void OnPlayerExitLevel()
        {
            _view.ExitLevel -= OnPlayerExitLevel;
            _movementController.StopMovement();
            _view.TurnOffBody();
            ServiceLocator.Instance.GetService<EventsService>().Fire(new ExitLevelSignal());
        }
    }
}