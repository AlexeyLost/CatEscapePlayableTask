using UnityEngine;

namespace GAME.Scripts.Guards
{
    /// <summary>
    /// Guard state that turn guard to the point.
    /// </summary>
    public class GuardTurnToPointState : GuardStateBase
    {
        private GuardBehaviourData _behaviourData;
        private int _targetPointIndex;
        
        public GuardTurnToPointState(GuardStateController controller, GuardStateType stateType, 
            GuardView view, GuardBehaviourData behaviourData) : base(controller, stateType, view)
        {
            _behaviourData = behaviourData;
            _targetPointIndex = -1;
        }

        public override void OnEnter(IStateData stateData = null)
        {
            _targetPointIndex = (_targetPointIndex + 1) % _behaviourData.PatrolPointsTransforms.Count;
            _isStateActive = true;
        }

        public override void OnExit()
        {
            _isStateActive = false;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_isStateActive) return;

            Vector3 targetPoint = _behaviourData.PatrolPointsTransforms[_targetPointIndex].position;
            Vector3 directionToPoint = (targetPoint - _view.transform.position).normalized;
            float angle = Vector3.Angle(directionToPoint, _view.transform.forward);
            if (Mathf.Abs(angle) > 0)
            {
                Quaternion newRotation = Quaternion.LookRotation(directionToPoint, Vector3.up);
                _view.transform.rotation = Quaternion.RotateTowards(_view.transform.rotation, 
                    newRotation, _view.TurnSpeed);
            }
            else
            {
                _controller.SetState(GuardStateType.MoveToPoint, new TargetPointStateData(_targetPointIndex));
            }
        }
    }
}