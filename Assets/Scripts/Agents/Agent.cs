using UnityEngine;

namespace Game
{
    
    public class Agent : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}