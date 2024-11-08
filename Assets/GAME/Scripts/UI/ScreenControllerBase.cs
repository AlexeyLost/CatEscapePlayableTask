namespace GAME.Scripts.UI
{
    /// <summary>
    /// Base class for screens controllers.
    /// </summary>
    public abstract class ScreenControllerBase
    {
        protected ScreenViewBase _screenView;
        
        
        public ScreenControllerBase(ScreenViewBase screenView)
        {
            _screenView = screenView;
        }

        public abstract void Show();
        public abstract void Hide();
    }
}