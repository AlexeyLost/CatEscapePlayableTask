namespace GAME.Scripts.Guards
{
    /// <summary>
    /// Base class of guards state.
    /// </summary>
    public abstract class GuardStateBase
    {
        protected GuardStateController _controller;
        protected GuardView _view;
        protected bool _isStateActive;
        
        public GuardStateType StateType { get; }
        
        public GuardStateBase(GuardStateController controller, GuardStateType stateType, GuardView view) {
            _controller = controller;
            StateType = stateType;
            _view = view;
            _isStateActive = false;
        }
        
        public abstract void OnEnter(IStateData stateData = null);
        public abstract void OnExit();
        public abstract void OnUpdate(float deltaTime);
    }
}