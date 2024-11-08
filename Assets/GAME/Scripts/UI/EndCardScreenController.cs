using UnityEngine;

namespace GAME.Scripts.UI
{
    public class EndCardScreenController : ScreenControllerBase
    {
        private EndCardScreenView _endCardScreenView;
        
        public EndCardScreenController(ScreenViewBase screenView) : base(screenView)
        {
            _endCardScreenView = (EndCardScreenView)screenView;
        }

        public override void Show()
        {
            _endCardScreenView.EndCardButtonClicked += OnEndCardClicked;
            _endCardScreenView.Show();
        }

        public override void Hide()
        {
            _endCardScreenView.EndCardButtonClicked -= OnEndCardClicked;
            _endCardScreenView.Hide();
        }

        private void OnEndCardClicked()
        {
            Application.OpenURL(_endCardScreenView.EndCardUrl);
        }
    }
}