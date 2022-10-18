using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayableAgentFactory", menuName = "Components/Playable Agent Factory")]
    public class PlayableAgentFactory : ScriptableObject
    {
        public PlayableAgentState CreateAgent(PlayableAgentData agentData)
        {
            return new PlayableAgentState(agentData);
        }
    }
}