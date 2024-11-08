using UnityEngine;

namespace GAME.Scripts.UI
{
    public class GameCompleteScreenController : ScreenControllerBase
    {
        private GameCompleteScreenView _gameCompleteScreenView;
        
        public GameCompleteScreenController(ScreenViewBase screenView) : base(screenView)
        {
            _gameCompleteScreenView = (GameCompleteScreenView)screenView;
        }

        public override void Show()
        {
            _gameCompleteScreenView.PlayButtonClicked += OnPlayButtonClicked;
            _gameCompleteScreenView.Show();
        }

        public override void Hide()
        {
            _gameCompleteScreenView.PlayButtonClicked -= OnPlayButtonClicked;
            _screenView.Hide();
        }

        private void OnPlayButtonClicked()
        {
            Application.OpenURL(_gameCompleteScreenView.ApplicationPageURL);
        }
    }
}