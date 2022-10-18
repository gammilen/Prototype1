using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayableAgent", menuName = "Agents/Playable Agent")]
    public class PlayableAgentData : Agent
    {
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Health = Mathf.Abs(Health);
            if (Health == 0)
            {
                Health = 1;
            }
            Speed = Mathf.Abs(Speed);
        }
#endif
    }

}