using System;
using System.Collections.Generic;

namespace GAME.Scripts.Core.Services
{
    /// <summary>
    /// Global events service
    /// </summary>
    public class EventsService : IService
    {
        private Dictionary<Type, List<Delegate>> _events = new();

        public void Subscribe<T>(Action<T> listener) where T : struct
        {
            if (!_events.ContainsKey(typeof(T)))
            {
                _events[typeof(T)] = new List<Delegate>();
            }
            _events[typeof(T)].Add(listener);
        }

        public void Unsubscribe<T>(Action<T> listener) where T : struct
        {
            if (_events.ContainsKey(typeof(T)))
            {
                _events[typeof(T)].Remove(listener);
                if (_events[typeof(T)].Count == 0)
                {
                    _events.Remove(typeof(T));
                }
            }
        }

        public void Fire<T>(T eventData) where T : struct
        {
            if (_events.ContainsKey(typeof(T)))
            {
                foreach (Delegate listener in _events[typeof(T)])
                {
                    (listener as Action<T>)?.Invoke(eventData);
                }
            }
        }    }
}