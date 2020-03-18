/*********************************************************************************************
* Based on Ryan Hipples talk: https://unity.com/how-to/architect-game-code-scriptable-objects
* Implementation based on Dapper Dino's indepth explanation
* Dapper Dino Patreon: https://www.patreon.com/dapperdino
* Dapper Dino YouTube: https://www.youtube.com/channel/UCjCpZyil4D8TBb5nVTMMaUw
**********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandorabots.Events
{
    [CreateAssetMenu]
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private List<IGameEventListener<T>> eventListeners =
            new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }

        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}