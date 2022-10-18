using UnityEngine;

namespace Game
{
    public interface IEscapeProcessor
    {
        void Escape(PlayableAgentState agentState);
    }

    [CreateAssetMenu(fileName ="AgentsEscape", menuName ="Components/Agents Escape")]
    public class AgentsEscape : ScriptableObject, IEscapeProcessor
    {
        [SerializeField] private LevelAgentsRegistry _levelAgents;
        [SerializeField] private AgentsEscapedChannel _agentsEscapedChannel;

        public void Escape(PlayableAgentState agentState)
        {
            agentState.SetEscaped();
            CheckEscapedAll();
        }

        private bool AllAgentsEscaped()
        {
            foreach (var agent in _levelAgents.PlayableAgents)
            {
                if (!agent.IsEscaped)
                {
                    return false;
                }
            }
            return true;
        }

        private void CheckEscapedAll()
        {
            if (AllAgentsEscaped())
            {
                _agentsEscapedChannel.Raise();
            }
        }
        
    }
}