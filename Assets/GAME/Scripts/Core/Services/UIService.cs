using System.Collections.Generic;
using GAME.Scripts.UI;
using UnityEngine;

namespace GAME.Scripts.Core.Services
{
    /// <summary>
    /// Service to use ui screens.
    /// </summary>
    public class UIService : MonoBehaviour, IService, IInitializeable
    {
        [SerializeField] private List<ScreenViewBase> screenViews = new();
        
        
        private Dictionary<ScreenType, ScreenControllerBase> _screenControllers = new();
        
        private Dictionary<ScreenType, ScreenControllerBase> _activeScreens = new();
        
        
        public void Initialize()
        {
            foreach (ScreenViewBase screenView in screenViews)
            {
                ScreenControllerBase screenController = screenView.ScreenType switch
                {
                    ScreenType.GameComplete => new GameCompleteScreenController(screenView),
                    ScreenType.Tutorial => new TutorialScreenController(screenView),
                    ScreenType.EndCard => new EndCardScreenController(screenView),
                    _ => null,
                };
                
                _screenControllers.Add(screenView.ScreenType, screenController);
            }
        }
        
        public void ShowScreen(ScreenType screenType)
        {
            if (_activeScreens.ContainsKey(screenType)) return;
            
            ScreenControllerBase screenToShow = _screenControllers[screenType];
            screenToShow.Show();
            _activeScreens.Add(screenType, screenToShow);
        }

        public void HideScreen(ScreenType screenType)
        {
            if (_activeScreens.TryGetValue(screenType, out ScreenControllerBase screenToHide))
            {
                screenToHide.Hide();
                _activeScreens.Remove(screenType);
            }
        }
    }
}