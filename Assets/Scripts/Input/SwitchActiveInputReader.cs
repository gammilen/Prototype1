using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SwitchActiveInputReader", menuName= "Input/Switch Active")]
    public class SwitchActiveInputReader : ScriptableObject
    {
        [SerializeField] private KeyCode _key = KeyCode.Tab;
        public bool GetSwitch()
        {
            return Input.GetKeyDown(_key);
        }
    }
}