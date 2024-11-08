using System.Collections;
using UnityEngine;

namespace GAME.Scripts.UI
{
    /// <summary>
    /// Base class for screen view.
    /// </summary>
    public class ScreenViewBase : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] private float _showAnimationDuration;

        [field: SerializeField] public ScreenType ScreenType { get; private set; }

        private float _currentShowTime;
        

        public virtual void Show()
        {
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            StartCoroutine(ShowAnimation());
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private IEnumerator ShowAnimation()
        {
            _currentShowTime = 0f;
            while (_currentShowTime < _showAnimationDuration)
            {
                _currentShowTime += Time.deltaTime;
                float alphaValue = Mathf.Lerp(0f, 1f, _currentShowTime / _showAnimationDuration);
                _canvasGroup.alpha = alphaValue;
                yield return null;
            }

            _canvasGroup.alpha = 1f;
        }
    }
}