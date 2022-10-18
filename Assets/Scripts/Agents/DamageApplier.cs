using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "DamageApplier", menuName = "Components/Damage Applier")]
    public class DamageApplier : ScriptableObject, IDamageApplier
    {
        [SerializeField] private AgentsHealthChange _healthChange;

        public void Apply(int amount, IPlayableAgent playableAgent)
        {
            _healthChange.ChangeHealth(playableAgent, -amount);
        }
    }
}