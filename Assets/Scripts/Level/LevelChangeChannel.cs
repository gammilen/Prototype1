using System;
using UnityEngine;

namespace Game
{
    public class LevelChangeChannel : ScriptableObject
    {
        [SerializeField] private InGameLevel _level;
        public event Action<Level> LevelChanged;

        public void Raise(Level level)
        {
            LevelChanged?.Invoke(level);
        }
    }
}