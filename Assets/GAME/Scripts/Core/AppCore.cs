using GAME.Scripts.Core.Services;
using GAME.Scripts.Events;
using GAME.Scripts.Level;
using GAME.Scripts.UI;
using UnityEngine;

namespace GAME.Scripts.Core 
{
    /// <summary>
    /// App core, here entry point to application, initialization and spawn level.
    /// </summary>
    public class AppCore : MonoBehaviour
    {
        [SerializeField] private PrefabsData _prefabsData;
        [SerializeField] private TickableService _tickableService;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private UIService _uiService;
        [SerializeField] private ScreenType _firstScreenToShow;
        [SerializeField] private GameMode _gameMode;
        
        private LevelController _levelController;
        
        
        private void Start()
        {
            InitializeServices();
            SpawnLevel();
            
            _uiService.ShowScreen(_firstScreenToShow);
            ServiceLocator.Instance.GetService<EventsService>().
                Subscribe<StartGameSignal>(OnStartGameSignalReceived);
            ServiceLocator.Instance.GetService<EventsService>().
                Subscribe<ExitLevelSignal>(OnExitLevelSignalReceived);
            
            _levelController.StartGame();
        }

        private void OnDisable()
        {
            ServiceLocator.Instance.GetService<EventsService>().
                Unsubscribe<StartGameSignal>(OnStartGameSignalReceived);
            ServiceLocator.Instance.GetService<EventsService>().
                Unsubscribe<ExitLevelSignal>(OnExitLevelSignalReceived);
        }

        private void InitializeServices()
        {
            ServiceLocator.Instance.RegisterService<TickableService>(_tickableService);
            ServiceLocator.Instance.RegisterService<EventsService>(new EventsService());
            ServiceLocator.Instance.RegisterService<PlayerInputService>(new PlayerInputService(_joystick));
            ServiceLocator.Instance.RegisterService<UIService>(_uiService);
        }

        private void SpawnLevel()
        {
            _levelController = new LevelController(_gameMode);
            _levelController.SpawnLevel(
                _prefabsData.LevelViewPrefab, 
                _prefabsData.PlayerViewPrefab, 
                _prefabsData.GuardViewPrefab);
            _levelController.GameOver += OnGameOver;
        }

        private void OnGameOver(bool win)
        {
            _levelController.GameOver -= OnGameOver;
            _uiService.ShowScreen(ScreenType.GameComplete);
        }

        private void OnStartGameSignalReceived(StartGameSignal signal)
        {
            _uiService.HideScreen(ScreenType.Tutorial);
        }

        private void OnExitLevelSignalReceived(ExitLevelSignal signal)
        {
            _uiService.ShowScreen(ScreenType.GameComplete);
        }
    }
}
