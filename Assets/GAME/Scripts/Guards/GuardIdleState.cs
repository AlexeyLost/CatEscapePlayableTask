namespace GAME.Scripts.Guards
{
    /// <summary>
    /// State that do nothing in my case. May be it should turn on idle animation.
    /// But we don't have it :)
    /// </summary>
    public class GuardIdleState : GuardStateBase
    {
        public GuardIdleState(GuardStateController controller, GuardStateType stateType, GuardView view) : base(controller, stateType, view)
        {
        }

        public override void OnEnter(IStateData stateData = null)
        {
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
        }
    }
}