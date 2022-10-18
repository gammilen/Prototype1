using UnityEditor;
using UnityEngine;

namespace Game
{
    public class PointsPositionResolver
    {
        public bool TryResolvePoint(Level level, Point point, out Vector3 position)
        {
            foreach (var p in level.Points.Points)
            {
                if (p.Point == point)
                {
                    position = p.Position;
                    return true;
                }
            }
            position = default;
            return false;
        }

        public Vector3 ResolvePoint(Level level, Point point)
        {
            foreach (var p in level.Points.Points)
            {
                if (p.Point == point)
                {
                    return p.Position;
                }
            }
            return default;
        }
    }
}