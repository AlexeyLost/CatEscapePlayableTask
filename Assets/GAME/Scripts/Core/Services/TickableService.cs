using System.Collections.Generic;
using UnityEngine;

namespace GAME.Scripts.Core.Services
{
    /// <summary>
    /// Service that helps to use one update method for all needs.
    /// </summary>
    public class TickableService : MonoBehaviour, IService
    {
        private HashSet<IUpdateable> _updateables = new();
        private HashSet<IFixedUpdateable> _fixedUpdateables = new();
        private HashSet<ILateUpdateable> _lateUpdateables = new();
        
        public void AddUpdateable(IUpdateable updateable)
        {
            if (!_updateables.Add(updateable))
            {
                Debug.LogError("Trying to add duplicate updateable");
            }
        }

        public void RemoveUpdateable(IUpdateable updateable)
        {
            if (!_updateables.Remove(updateable))
            {
                Debug.LogError($"Trying to remove unregistered updateable");
            }
        }

        public void AddFixedUpdateable(IFixedUpdateable fixedUpdateable)
        {
            if (!_fixedUpdateables.Add(fixedUpdateable))
            {
                Debug.LogError("Trying to add duplicate fixed updateable");
            }
        }

        public void RemoveFixedUpdateable(IFixedUpdateable fixedUpdateable)
        {
            if (!_fixedUpdateables.Remove(fixedUpdateable))
            {
                Debug.LogError($"Trying to remove unregistered fixed updateable");
            }
        }
        
        public void AddLateUpdateable(ILateUpdateable lateUpdateable)
        {
            if (!_lateUpdateables.Add(lateUpdateable))
            {
                Debug.LogError("Trying to add duplicate late updateable");
            }
        }

        public void RemoveLateUpdateable(ILateUpdateable lateUpdateable)
        {
            if (!_lateUpdateables.Remove(lateUpdateable))
            {
                Debug.LogError($"Trying to remove unregistered late updateable");
            }
        }

        private void Update()
        {
            foreach (IUpdateable updateable in _updateables) 
                updateable.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            foreach (IFixedUpdateable fixedUpdateable in _fixedUpdateables)
                fixedUpdateable.FixedUpdate(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            foreach (ILateUpdateable lateUpdateable in _lateUpdateables)
                lateUpdateable.LateUpdate(Time.deltaTime);
        }
    }
}