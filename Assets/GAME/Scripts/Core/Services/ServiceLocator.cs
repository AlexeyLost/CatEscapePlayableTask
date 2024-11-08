using System;
using System.Collections.Generic;
using UnityEngine;

namespace GAME.Scripts.Core.Services
{
    /// <summary>
    /// Singleton service locator that store services
    /// </summary>
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();
        
        private Dictionary<string, IService> _services = new ();
        

        public void RegisterService<T>(IService service) where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
            {
                Debug.LogError($"Service {key} already registered");
                return;
            }
            
            _services.Add(key, service);
            RegisterUpdateables(service);
        }

        public void UnregisterService<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key)) _services.Remove(key);
        }

        public T GetService<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.TryGetValue(key, out var service))
            {
                Debug.LogError($"Service {key} not registered");
                throw new InvalidOperationException();
            }

            return (T)service;
        }

        private void RegisterUpdateables(IService service)
        {
            if (!_services.ContainsKey(nameof(TickableService)))
            {
                Debug.LogError($"Service {nameof(TickableService)} not registered but you try " +
                               $"to register new service {nameof(service)} that should use ticks");
                return;
            }

            if (service is IInitializeable iInitializable)
            {
                iInitializable.Initialize();
            }
            
            TickableService tickableService = GetService<TickableService>();
            if (service is IUpdateable updateable)
            {
                tickableService.AddUpdateable(updateable);
            }

            if (service is IFixedUpdateable fixedUpdateable)
            {
                tickableService.AddFixedUpdateable(fixedUpdateable);
            }
            
            if (service is ILateUpdateable lateUpdateable)
            {
                tickableService.AddLateUpdateable(lateUpdateable);
            }
        }

        private void UnregisterUpdateables(IService service)
        {
            if (!_services.ContainsKey(nameof(TickableService)))
            {
                Debug.LogError($"Service {nameof(TickableService)} not registered but you try " +
                               $"to unregister service {nameof(service)} from tickables");
                return;
            }
            
            TickableService tickableService = GetService<TickableService>();
            if (service is IUpdateable updateable)
            {
                tickableService.RemoveUpdateable(updateable);
            }

            if (service is IFixedUpdateable fixedUpdateable)
            {
                tickableService.RemoveFixedUpdateable(fixedUpdateable);
            }
            
            if (service is ILateUpdateable lateUpdateable)
            {
                tickableService.RemoveLateUpdateable(lateUpdateable);
            }
        }
    }
}