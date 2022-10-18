using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelMap", menuName = "Entities/Level Map")]
    public class LevelMap : ScriptableObject
    {
        [field: SerializeField] public Point EscapePoint { get; private set; }
        [SerializeField] private List<LevelAgentPoint> _agents;
        [SerializeField] private List<LevelAgentTrajectory> _trajectories;

        public IEnumerable<(Agent agent, Point point, IList<Point> trajectory)> GetAgentsData()
        {
            foreach (var agent in _agents)
            {
                yield return (agent.Agent, agent.Point, GetTrajectory(agent.Agent));
            }
        }

        private IList<Point> GetTrajectory(Agent agent)
        {
            foreach (var trajectoryData in _trajectories)
            {
                if (trajectoryData.Agent == agent)
                {
                    return trajectoryData.Trajectory;
                }
            }
            return new List<Point>();
        }
    }
}