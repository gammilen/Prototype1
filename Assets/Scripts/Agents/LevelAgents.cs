using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelAgents", menuName = "Components/Level Agents")]
    public class LevelAgents : StateHolderScriptableObject
    {
        [HideInInspector][SerializeField] private List<PlayableAgentState> _playableAgents = new List<PlayableAgentState>();
        [SerializeField] private List<IAutonomousAgentState> _enemyAgents = new List<IAutonomousAgentState>();
        public IList<PlayableAgentState> PlayableAgents => _playableAgents;
        public IList<IAutonomousAgentState> EnemyAgents => _enemyAgents;

        public void ResetAgents()
        {
            _playableAgents.Clear();
        }
        public void AddPlayableAgent(PlayableAgentState agent)
        {
            _playableAgents.Add(agent);
        }

        public void AddEnemyAgent(IAutonomousAgentState agent)
        {
            _enemyAgents.Add(agent);
        }

        public override void SetData(object data)
        {
            var state = data as LevelAgents;
            _playableAgents = new List<PlayableAgentState>(state.PlayableAgents);
            _enemyAgents = new List<IAutonomousAgentState>(state.EnemyAgents);
        }
    }
}