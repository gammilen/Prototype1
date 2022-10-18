using System.Collections;
using UnityEngine;

namespace Visual
{
    public class LevelPlayArea : MonoBehaviour, IAgentsPositionsSurface
    {
        [SerializeField] private Bounds _playAreaBounds;

        public Vector3 MoveTowards(Vector3 fromPosition, Vector3 toPosition, float distance)
        {
            return _playAreaBounds.ClosestPoint(fromPosition + Vector3.Normalize(toPosition - fromPosition) * distance);
        }

        public Vector3 GetNearestPosition(Vector3 position)
        {
            return _playAreaBounds.ClosestPoint(position);
        }
    }
}