using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Visual
{
    public class PlayableAgentSpawner : MonoBehaviour
    {
        [SerializeField] private LevelAgentsRegistry _registry;
        [SerializeField] private AgentsEscape _escape;
        [SerializeField] private PlayableAgentsMove _move; 
        [SerializeField] private LevelPlayArea _playArea;
        [SerializeField] private Transform _root;
        private HashSet<PlayableAgent> _agents;
        

        private void Start()
        {
            _agents = new HashSet<PlayableAgent>();
            foreach (var agent in _registry.PlayableAgents)
            {
                var agentObj = Instantiate(agent.Data.Prefab, _root).GetComponent<PlayableAgent>();
                agentObj.Init(agent, _playArea, _escape, _move);
                _agents.Add(agentObj);
            }
        }
    }
}