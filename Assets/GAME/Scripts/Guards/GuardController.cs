using System;
using GAME.Scripts.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GAME.Scripts.Guards
{
    /// <summary>
    /// Brain of guards.
    /// </summary>
    public class GuardController : IUpdateable, ILateUpdateable
    {
        public event Action PlayerDetected;
        
        
        private GuardView _view;
        private GuardStateController _stateController;
        private GuardBehaviourData _guardBehaviourData;
        private GuardPlayerDetectorController _playerDetectorController;

        public GuardController(GuardBehaviourData guardBehaviourData)
        {
            _guardBehaviourData = guardBehaviourData;
        }

        public void Init(GuardView viewPrefab, Transform parentTransform)
        {
            SpawnView(viewPrefab, parentTransform, _guardBehaviourData.transform);
            CreateStateController();
            if (_guardBehaviourData.UsePlayerDetector) CreatePlayerDetector();
        }

        public void Update(float deltaTime)
        {
            _stateController.Update(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            if (_guardBehaviourData.UsePlayerDetector) _playerDetectorController.LateUpdate(deltaTime);
        }
        
        private void SpawnView(GuardView viewPrefab, Transform parentTransform, Transform spawnPointTransform)
        {
            _view = Object.Instantiate(viewPrefab, spawnPointTransform.position, spawnPointTransform.rotation);
            _view.transform.SetParent(parentTransform);
        }

        private void CreateStateController()
        {
            _stateController = new GuardStateController(_view, _guardBehaviourData);
        }

        private void CreatePlayerDetector()
        {
            _playerDetectorController = new GuardPlayerDetectorController(_view.PlayerDetectorView);
            _playerDetectorController.Init();
            _playerDetectorController.PlayerDetected += OnPlayerDetected;
            _playerDetectorController.StartDetection();
        }

        private void OnPlayerDetected(Vector3 playerPosition)
        {
            _playerDetectorController.StopDetection();
            _playerDetectorController.PlayerDetected -= OnPlayerDetected;
            _stateController.SetState(GuardStateType.Attack, new AttackStateData(playerPosition));
            PlayerDetected?.Invoke();
        }
    }
}
