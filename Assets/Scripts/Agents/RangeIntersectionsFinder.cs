using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "RangeIntersectionsFinder", menuName = "Components/Range Intersections Finder")]
    public class RangeIntersectionsFinder : ScriptableObject, IRangeIntersectionsObserver
    {
        [SerializeField] private LevelAgents _agents;

        public void AddIntersectionListener(IMoveState agent, float range, IRangeIntersectionsObserver.IntersectionAction action)
        { }

        public bool TryGetIntersection(Vector3 position, float range, out IPlayableAgent playableAgent)
        {
            range *= range;
            foreach (var a in _agents.PlayableAgents)
            {
                var diff = a.Position - position;
                diff.y = 0;
                if (diff.sqrMagnitude <= range)
                {
                    playableAgent = a;
                    return true;
                }
            }
            playableAgent = default;
            return false;
        }
    }
}