using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AgentsEscapedChannel", menuName ="Events/Escape")]
    public class AgentsEscapedChannel : ScriptableObject
    {
        public event Action EscapeEvent;

        public void Raise()
        {
            EscapeEvent?.Invoke();
        }
    }
}