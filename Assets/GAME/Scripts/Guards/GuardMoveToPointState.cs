using UnityEngine;

namespace GAME.Scripts.Guards
{
    /// <summary>
    /// Guards state that move guard to the point.
    /// </summary>
    public class GuardMoveToPointState : GuardStateBase
    {
        private GuardBehaviourData _guardBehaviourData;
        private Vector3 _targetPosition;
        
        public GuardMoveToPointState(GuardStateController controller, GuardStateType stateType, 
            GuardView view, GuardBehaviourData guardBehaviourData) : base(controller, stateType, view)
        {
            _guardBehaviourData = guardBehaviourData;
        }

        public override void OnEnter(IStateData stateData = null)
        {
            TargetPointStateData currentData = (TargetPointStateData)stateData;
            _targetPosition = _guardBehaviourData.PatrolPointsTransforms[currentData.TargetPointIndex].position;
            _isStateActive = true;
        }

        public override void OnExit()
        {
            _isStateActive = false;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_isStateActive) return;
            
            float distanceToTarget = Vector3.Distance(_view.transform.position, _targetPosition);

            if (distanceToTarget > _guardBehaviourData.MinDistanceToTarget)
            {
                Vector3 directionToTarget = (_targetPosition - _view.transform.position).normalized;
                _view.transform.position += directionToTarget * (deltaTime * _guardBehaviourData.MoveSpeed);
            }
            else
            {
                _controller.SetState(GuardStateType.TurnToPoint);
            }
        }
    }
}