using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    

    [CreateAssetMenu(fileName = "LevelAgentsRegistry", menuName = "Components/Level Agents Registry")]
    public class LevelAgentsRegistry : ScriptableObject, ITargetsSource // TODO: Remove
    {
        [SerializeField] private InGameLevel _level;
        [SerializeField] private PlayableAgentFactory _playableAgentsFactory;
        [SerializeField] private AgentsFactories _autoAgentsFactories;
        [SerializeField] private LevelAgents _levelAgents;
        private PointsPositionResolver _pointsResolver = new PointsPositionResolver();

        public IList<PlayableAgentState> PlayableAgents => _levelAgents.PlayableAgents;
        public IList<IAutonomousAgentState> EnemyAgents => _levelAgents.EnemyAgents;

        public void Register(LevelMap map)
        {
            _levelAgents.ResetAgents();
            var emptyTrajectory = new List<Vector3>();
            foreach (var levelAgent in map.GetAgentsData())
            {
                if (levelAgent.agent is PlayableAgentData playableAgentData) // todo: to interface
                {
                    var agent = _playableAgentsFactory.CreateAgent(playableAgentData);
                    agent.SetPosition(_pointsResolver.ResolvePoint(_level.CurrentLevel, levelAgent.point));
                    _levelAgents.AddPlayableAgent(agent);
                }
                else
                {
                    var factory = _autoAgentsFactories.GetFactory(levelAgent.agent);
                    var agent = factory.CreateAgentState(levelAgent.agent, 
                        _pointsResolver.ResolvePoint(_level.CurrentLevel, levelAgent.point), 
                        ResolveTrajectory(levelAgent.trajectory));
                    _levelAgents.AddEnemyAgent(agent);
                }
            }
        }

        private IList<Vector3> ResolveTrajectory(IList<Point> trajectory)
        {
            var positions = new List<Vector3>();
            foreach (var point in trajectory)
            {
                positions.Add(_pointsResolver.ResolvePoint(_level.CurrentLevel, point));
            }
            return positions;
        }

        IEnumerable<IMoveState> ITargetsSource.GetTargets()
        {
            foreach (var agent in PlayableAgents)
            {
                yield return agent;
            }
        }
    }
}