using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="LevelFinish", menuName ="Components/Level Finish")]
    public class LevelFinish : ScriptableObject
    {
        [SerializeField] private AgentsEscapedChannel _agentsEscaped;
        [SerializeField] private AgentDeathChannel _agentDeathChannel;
        [SerializeField] private LevelPreparer _levelPreparer;
        [SerializeField] private GameLevels _levels;
        [SerializeField] private InGameLevel _level;

        public event Action<bool> FinishEvent;

        private void OnEnable()
        {
            _agentsEscaped.EscapeEvent += FinishLevel;
            _agentDeathChannel.DeathEvent += RestartLevel;
        }

        private void FinishLevel()
        {
            FinishEvent?.Invoke(true);
            
        }

        private void RestartLevel()
        {
            FinishEvent?.Invoke(false);
            _levelPreparer.ChangeLevel(_level.CurrentLevel);
        }
    }
}