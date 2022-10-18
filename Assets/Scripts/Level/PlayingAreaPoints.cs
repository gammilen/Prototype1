using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayingAreaPoints", menuName = "Entities/Playing Area Points")]
    public class PlayingAreaPoints : ScriptableObject
    {
        [System.Serializable]
        public class PointPoisition
        {
            [field: SerializeField] public Point Point { get; private set; }
            [field: SerializeField] public Vector3 Position { get; private set; }
        }

        [SerializeField] private List<PointPoisition> _points;

        public IEnumerable<PointPoisition> Points => _points;
    }
}