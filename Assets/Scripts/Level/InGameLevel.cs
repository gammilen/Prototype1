using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

namespace Game
{
    [CreateAssetMenu(fileName = "InGameLevel", menuName ="Entities/InGame Level")]
    public class InGameLevel : StateHolderScriptableObject
    {
        [JsonConverter(typeof(SOConverter<Level>))]
        [HideInInspector][SerializeField] public Level _currentLevel;
        //[SerializeField] private LevelChangeChannel _channel;

        [JsonIgnore] public Level CurrentLevel => _currentLevel;

        public void ChangeLevel(Level level)
        {
            Debug.Log(JsonUtility.ToJson(this));
            _currentLevel = level;
            //_channel.Raise(_currentLevel);
        }

        public override void SetData(object data)
        {
            _currentLevel = (data as InGameLevel).CurrentLevel;
        }
    }
}