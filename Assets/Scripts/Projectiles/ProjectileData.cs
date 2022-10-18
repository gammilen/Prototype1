using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName ="Projectile", menuName ="Data/Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float FlightTime { get; private set; }
        [field: SerializeField] public float CollisionDistance { get; private set; }

    }
}