using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AgentsHealthChange", menuName = "Components/Agents Health Change")]
    public class AgentsHealthChange : ScriptableObject
    {
        [SerializeField] private AgentDeathChannel _agentDeathChannel;
        public event Action<IPlayableAgent> ChangeEvent;

        public void ChangeHealth(IPlayableAgent agent, int amount)
        {
            if (agent is IHealthState health && agent is IEscapeState escapeState && !escapeState.IsEscaped)
            {
                health.Health += amount;
                ChangeEvent?.Invoke(agent);
                if (health.Health <= 0)
                {
                    _agentDeathChannel.Raise();
                }
            }
        }
    }
}