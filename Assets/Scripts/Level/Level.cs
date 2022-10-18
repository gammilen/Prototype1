using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="Level", menuName ="Entities/Level")]
    public class Level : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
        [field: SerializeField] public LevelMap Map { get; private set; }
        [field: SerializeField] public PlayingAreaPoints Points { get; private set; }

        public void Validate()
        {
            // load scene and its play area to check positions existence
        }
    }
}