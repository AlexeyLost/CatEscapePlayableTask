using System;
using UnityEngine;
using UnityEngine.UI;

namespace GAME.Scripts.UI
{
    public class EndCardScreenView : ScreenViewBase
    {
        [SerializeField] private Button _endCardButton;
        [field: SerializeField] public string EndCardUrl { get; private set; }

        public event Action EndCardButtonClicked; 
        
        
        public override void Show()
        {
            _canvasGroup.alpha = 1f;
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _endCardButton.onClick.AddListener(OnEndCardClicked);
        }

        private void OnDisable()
        {
            _endCardButton.onClick.RemoveListener(OnEndCardClicked);
        }

        private void OnEndCardClicked()
        {
            EndCardButtonClicked?.Invoke();
        }
    }
}