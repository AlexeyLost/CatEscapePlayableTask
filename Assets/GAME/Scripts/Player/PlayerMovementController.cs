using GAME.Scripts.Core.Services;
using UnityEngine;

namespace GAME.Scripts.Player
{
    /// <summary>
    /// Controller that moves player body depends on input.
    /// </summary>
    public class PlayerMovementController
    {
        private PlayerInputService _inputService;
        private PlayerView _view;
        private bool _canMove;
        
        public PlayerMovementController(PlayerInputService inputService, PlayerView view)
        {
            _inputService = inputService;
            _view = view;
            _canMove = true;
        }

        public void UpdateMovement(float fixedDeltaTime)
        {
            if (!_canMove) return;
            
            if (_inputService.InputVector != Vector2.zero)
            {
                Vector3 moveVector = new Vector3(_inputService.InputVector.x, 0, 
                    _inputService.InputVector.y) * fixedDeltaTime;
                
                _view.Move(moveVector);
            }
            else
            {
                _view.StopMovement();
            }
        }

        public void StopMovement()
        {
            _canMove = false;
            _view.StopMovement();
        }
    }
}