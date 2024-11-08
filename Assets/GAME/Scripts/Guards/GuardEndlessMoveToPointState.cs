using UnityEngine;

namespace GAME.Scripts.Guards
{
    /// <summary>
    /// Guards state, that made for end game card, just slow endless movement to point.
    /// </summary>
    public class GuardEndlessMoveToPointState : GuardStateBase
    {
        private GuardBehaviourData _behaviourData;
        private Vector3 _targetPosition;
        private Vector3 _directionToPoint;
        private bool _rotatedToTarget;
        private float _moveSpeed;
        
        private const float DistanceSpeedMultiplier = 0.12f;

        
        public GuardEndlessMoveToPointState(GuardStateController controller, GuardStateType stateType, 
            GuardView view, GuardBehaviourData behaviourData) : base(controller, stateType, view)
        {
            _behaviourData = behaviourData;
        }

        public override void OnEnter(IStateData stateData = null)
        {
            _targetPosition = _behaviourData.PointForEndlessMove.position;
            _directionToPoint = (_targetPosition - _view.transform.position).normalized;
            _directionToPoint.y = 0;
            _rotatedToTarget = false;
            _moveSpeed = _behaviourData.MoveSpeed;
            _isStateActive = true;
        }

        public override void OnExit()
        {
            _isStateActive = false;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_isStateActive) return;

            if (!_rotatedToTarget) RotateToTarget();
            else MoveToTarget(deltaTime);
        }

        private void RotateToTarget()
        {
            float angle = Vector3.Angle(_directionToPoint, _view.transform.forward);
            if (Mathf.Approximately(Mathf.Abs(angle), 0f))
            {
                _rotatedToTarget = true;
                return;
            }
            
            Quaternion newRotation = Quaternion.LookRotation(_directionToPoint, Vector3.up);
            _view.transform.rotation = Quaternion.RotateTowards(_view.transform.rotation,
                newRotation, _view.TurnSpeed);
        }

        private void MoveToTarget(float deltaTime)
        {
            float distanceToTarget = Vector3.Distance(_view.transform.position, _targetPosition);
            _moveSpeed = distanceToTarget * DistanceSpeedMultiplier;

            if (distanceToTarget > _behaviourData.MinDistanceToTarget)
            {
                Vector3 directionToTarget = (_targetPosition - _view.transform.position).normalized;
                _view.transform.position += directionToTarget * (deltaTime * _moveSpeed);
            }
        }
    }
}