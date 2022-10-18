using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Services;


namespace Game
{
    [CreateAssetMenu(fileName ="LevelPreparer", menuName ="Components/Level Preparer")]
    public class LevelPreparer : ScriptableObject
    {
        [SerializeField] private InGameLevel _level;
        [SerializeField] private GameLevels _levels;
        [SerializeField] private LevelFinish _finish;
        [SerializeField] private LevelAgentsRegistry _agentsRegistry;
        [SerializeField] private PlayableAgentActivator _activator;
        [SerializeField] private ProjectilesLauncher _projectilesLauncher;
        [SerializeField] private SaveLoadService _saveLoadService;


        private void OnEnable()
        {
            _saveLoadService.DataLoaded += Reload;
            _finish.FinishEvent += HandleFinish;
        }

        private void HandleFinish(bool result)
        {
            if (result)
            {
                ChangeLevel(_levels.GetNextOrFirst(_level.CurrentLevel));
            }
            else
            {
                ChangeLevel(_level.CurrentLevel);
            }
        }

        private void Reload()
        {
            _activator.InitActive();
            Load();
        }

        public void ChangeFirst()
        {
            ChangeLevel(_levels.Levels[0]);
        }

        public void ChangeLevel(Level level)
        {
            _level.ChangeLevel(level);
            _agentsRegistry.Register(level.Map);
            _projectilesLauncher.ResetProjectiles();
            _activator.ActiveFirst();
            Load();
        }

        public void ChangeTestLevel(Level level)
        {
            
            _level.ChangeLevel(level);
            _agentsRegistry.Register(level.Map);
            _projectilesLauncher.ResetProjectiles();
            _activator.ActiveFirst();
            Load();
        }

        private void Load()
        {
            var scene = SceneManager.LoadSceneAsync(_level.CurrentLevel.SceneName);
        }
    }
}