using UnityEditor;
using UnityEngine;

namespace Game
{
    public interface IAgentMoveHandler
    {
        void HandleMove(PlayableAgentState agentState, Vector3 position);
    }
    [CreateAssetMenu(fileName = "PlayableAgentsMove ", menuName ="Components/Playable Agents Move")]
    public class PlayableAgentsMove : ScriptableObject, IAgentMoveHandler
    {
        public bool IsActive = true;

        public void HandleMove(PlayableAgentState agentState, Vector3 position)
        {
            if (IsActive)
            {
                agentState.SetPosition(position);
            }
        }
    }
}