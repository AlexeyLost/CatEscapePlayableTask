using GAME.Scripts.Core.Services;
using GAME.Scripts.Events;

namespace GAME.Scripts.UI
{
    public class TutorialScreenController : ScreenControllerBase
    {
        private TutorialScreenView _tutorialScreenView;
        
        public TutorialScreenController(ScreenViewBase screenView) : base(screenView)
        {
            _tutorialScreenView = (TutorialScreenView)screenView;
        }

        public override void Show()
        {
            _tutorialScreenView.StartGameClicked += OnStartGameClicked;
            _tutorialScreenView.Show();
        }

        public override void Hide()
        {
            _tutorialScreenView.StartGameClicked -= OnStartGameClicked;
            _tutorialScreenView.Hide();
        }

        private void OnStartGameClicked()
        {
            ServiceLocator.Instance.GetService<EventsService>().Fire(new StartGameSignal());
        }
    }
}