using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="GameLevels", menuName ="Entities/Game Levels")]
    public class GameLevels : ScriptableObject
    {
        [SerializeField] private List<Level> _levels;
        public IList<Level> Levels => _levels;

        public Level GetNextOrFirst(Level level)
        {
            if (_levels.Contains(level))
            {
                return _levels[(_levels.IndexOf(level) + 1) % _levels.Count];
            }
            return _levels[0];
        }
    }
}