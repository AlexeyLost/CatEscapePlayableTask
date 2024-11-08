using System;
using UnityEngine;

namespace GAME.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Animator _animator;

        private readonly int MovingAnimationKey = Animator.StringToHash("Moving");

        public event Action ExitLevel;
        
        
        public void Move(Vector3 movement)
        {
            movement.y = _rigidbody.position.y;
            movement *= _moveSpeed;
            Vector3 nextPosition = _rigidbody.position + movement;
            _rigidbody.MovePosition(nextPosition);
            RotateTowardsMovement(movement);
            if (!_animator.GetBool(MovingAnimationKey))
            {
                _animator.SetBool(MovingAnimationKey, true);
            }
        }

        public void StopMovement() 
        {
            if (_animator.GetBool(MovingAnimationKey))
            {
                _animator.SetBool(MovingAnimationKey, false);
            }
        }

        public void TurnOffBody()
        {
            _rigidbody.isKinematic = true;
        }

        private void RotateTowardsMovement(Vector3 moveDirection) 
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, newRotation, _rotationSpeed));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("LevelExit"))
            {
                ExitLevel?.Invoke();
            }
        }
    }
}