using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GAME.Scripts.UI
{
    public class GameCompleteScreenView : ScreenViewBase
    {
        [SerializeField] private Button _playeButton;
        
        [field: SerializeField] public string ApplicationPageURL { get; private set; }

        public event Action PlayButtonClicked; 
        

        private void OnEnable()
        {
            _playeButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            _playeButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _playeButton.interactable = false;
            PlayButtonClicked?.Invoke();
        }
    }
}