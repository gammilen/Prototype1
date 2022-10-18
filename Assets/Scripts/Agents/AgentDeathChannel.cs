using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AgentDeathChannel", menuName = "Events/Death")]
    public class AgentDeathChannel : ScriptableObject
    {
        public event Action DeathEvent;

        public void Raise()
        {
            DeathEvent?.Invoke();
        }
    }
}