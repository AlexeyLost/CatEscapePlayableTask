using UnityEngine;

namespace GAME.Scripts.Core.Services
{
    /// <summary>
    /// Service that observe joystick input
    /// </summary>
    public class PlayerInputService: IService, IUpdateable
    {
        private Joystick _joystick;

        public Vector2 InputVector { get; private set; }
        
        public PlayerInputService(Joystick joystick) 
        {
            _joystick = joystick;
        }

        public void Update(float deltaTime)
        {
            InputVector = _joystick.Direction;
        }
    }
}