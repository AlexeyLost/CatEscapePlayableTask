using System.Collections.Generic;

namespace GAME.Scripts.Guards
{
    /// <summary>
    /// States controller for guards, that create and switches states.
    /// </summary>
    public class GuardStateController
    {
        private GuardView _guardView;
        private Dictionary<GuardStateType, GuardStateBase> _states = new();
        private GuardStateBase _currentState;
        private GuardBehaviourData _guardBehaviourData;

        public GuardStateController(GuardView guardView, GuardBehaviourData guardBehaviourData)
        {
            _guardView = guardView; 
            _guardBehaviourData = guardBehaviourData;
            SetState(_guardBehaviourData.StartStateType);
        }
        
        public void SetState(GuardStateType newStateType, IStateData stateData = null) {
            if (!_states.TryGetValue(newStateType, out GuardStateBase newState)) {
                newState = CreateState(newStateType);
            }
            _currentState?.OnExit();
            newState.OnEnter(stateData);
            _currentState = newState;
        }

        public void Update(float deltaTime)
        {
            _currentState.OnUpdate(deltaTime);
        }

        
        private GuardStateBase CreateState(GuardStateType stateType) {
            GuardStateBase currentState = stateType switch {
                GuardStateType.Idle => new GuardIdleState(this, GuardStateType.Idle, _guardView),
                GuardStateType.TurnToPoint => new GuardTurnToPointState(this, GuardStateType.TurnToPoint, _guardView, _guardBehaviourData),
                GuardStateType.MoveToPoint => new GuardMoveToPointState(this, GuardStateType.MoveToPoint, _guardView, _guardBehaviourData), 
                GuardStateType.Attack => new GuardAttackState(this, GuardStateType.Attack, _guardView, _guardBehaviourData),
                GuardStateType.EndlessMoveToPoint => new GuardEndlessMoveToPointState(this, GuardStateType.EndlessMoveToPoint, _guardView, _guardBehaviourData),
                _ => new GuardIdleState(this, GuardStateType.Idle, _guardView)
            };
            _states.Add(stateType, currentState);

            return currentState;
        }
    }
}