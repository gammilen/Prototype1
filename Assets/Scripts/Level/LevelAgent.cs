using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class LevelAgentPoint
    {
        [field: SerializeField] public Agent Agent { get; private set; }
        [field: SerializeField] public Point Point { get; private set; }
    }

    [System.Serializable]
    public class LevelAgentTrajectory
    {
        [field: SerializeField] public Agent Agent { get; private set; }
        [SerializeField] private List<Point> _trajectory;
        public IList<Point> Trajectory => _trajectory;
    }
}