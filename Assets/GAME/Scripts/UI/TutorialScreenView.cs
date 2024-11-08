using System;
using UnityEngine;

namespace GAME.Scripts.UI
{
    public class TutorialScreenView : ScreenViewBase
    {
        private bool _gameStarted;
        
        public event Action StartGameClicked;
        
        
        public override void Show()
        {
            _gameStarted = false;
            _canvasGroup.alpha = 1f;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_gameStarted) return;

            if (Input.GetMouseButtonDown(0))
            {
                _gameStarted = true;
                StartGameClicked?.Invoke();
            }
        }
    }
}