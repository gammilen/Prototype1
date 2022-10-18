using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IPlayableAgentActivity
    {
        bool IsActive(PlayableAgentState agent);
    }

    [CreateAssetMenu(fileName = "PlayableAgentActivator", menuName = "Components/Playable Agent Activator")]
    public class PlayableAgentActivator : ScriptableObject, IPlayableAgentActivity
    {
        [SerializeField] private LevelAgentsRegistry _levelAgentsRegistry;
        public PlayableAgentState ActiveAgent { get; private set; }
        public event Action ActiveChangedEvent;
        // TODO: active agent changed channel

        public void ActivateAgent(PlayableAgentState agent)
        {
            if (!CanActivate(agent))
            {
                return;
            }
            foreach (var playableAgent in _levelAgentsRegistry.PlayableAgents)
            {
                playableAgent.SetActive(false);
            }
            ActiveAgent = agent;
            ActiveAgent.SetActive(true);
            ActiveChangedEvent?.Invoke();
        }

        public void ActiveFirst()
        {
            foreach (var playableAgent in _levelAgentsRegistry.PlayableAgents)
            {
                if (CanActivate(playableAgent))
                {
                    ActivateAgent(playableAgent);
                    return;
                }
            }
        }

        public void InitActive()
        {
            foreach (var agent in _levelAgentsRegistry.PlayableAgents)
            {
                if (agent.IsActive)
                {
                    ActivateAgent(agent);
                    return;
                }
            }
        }

        public void ActivateNext()
        {
            if (!_levelAgentsRegistry.PlayableAgents.Contains(ActiveAgent))
            {
                ActiveFirst();
                return;
            }
            var index = _levelAgentsRegistry.PlayableAgents.IndexOf(ActiveAgent) + 1;
            for (int i = 0; i < _levelAgentsRegistry.PlayableAgents.Count; i++)
            {
                var agent = _levelAgentsRegistry.PlayableAgents[(index + i) % _levelAgentsRegistry.PlayableAgents.Count];
                if (CanActivate(agent))
                {
                    ActivateAgent(agent);
                    return;
                }
            }
        }

        public bool IsActive(PlayableAgentState agent)
        {
            return ActiveAgent == agent;
        }

        private bool CanActivate(PlayableAgentState agent)
        {
            return _levelAgentsRegistry.PlayableAgents.Contains(agent);
        }
    }
}