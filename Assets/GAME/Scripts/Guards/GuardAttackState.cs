using System.Collections;
using UnityEngine;

namespace GAME.Scripts.Guards
{
    public class GuardAttackState : GuardStateBase
    {
        private GuardBehaviourData _guardBehaviourData;
        
        public GuardAttackState(GuardStateController controller, GuardStateType stateType, 
            GuardView view, GuardBehaviourData guardBehaviourData) : base(controller, stateType, view)
        {
            _guardBehaviourData = guardBehaviourData;
        }

        public override void OnEnter(IStateData stateData = null)
        {
            AttackStateData data = (AttackStateData)stateData;
            Vector3 playerPosition = data.TargetPosition;
            _isStateActive = true;
            _view.StartCoroutine(AttackCoroutine(playerPosition));
        }

        public override void OnExit()
        {
            _isStateActive = false;
        }

        public override void OnUpdate(float deltaTime)
        {
        }
        
        private IEnumerator AttackCoroutine(Vector3 playerPosition)
        {
            Vector3 directionToPoint = (playerPosition - _view.transform.position).normalized;
            directionToPoint.y = 0;

            
            float angle = Vector3.Angle(directionToPoint, _view.transform.forward);
            while (_isStateActive && Mathf.Abs(angle) > 0)
            {
                Quaternion newRotation = Quaternion.LookRotation(directionToPoint, Vector3.up);
                _view.transform.rotation = Quaternion.RotateTowards(_view.transform.rotation, 
                    newRotation, _view.TurnSpeed);
                angle = Vector3.Angle(directionToPoint, _view.transform.forward);
                yield return null;
            }
            
            while (_isStateActive && Vector3.Distance(_view.transform.position, playerPosition) > _view.AttackDistance)
            {
                _view.transform.position += directionToPoint * (Time.deltaTime * _guardBehaviourData.MoveSpeed);
                yield return null;
            }
            
            _view.SetAttackAnimation();
        }
    }
}