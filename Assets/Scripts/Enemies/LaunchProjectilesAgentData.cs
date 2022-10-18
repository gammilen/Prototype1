using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LaunchProjectilesAgentData", menuName = "Entities/Enemies/Launch Projectiles Data")]
    public class LaunchProjectilesAgentData : Agent, IThrowProjectilesAgentData
    {
        [System.Serializable]
        public class AttackProjectileData : AttackData, IAttackThrowingProjectilesData
        {
            public ProjectileData Projectile;
            public float Cooldown;
            ProjectileData IAttackThrowingProjectilesData.Projectile => Projectile;
            float IAttackCooldown.Cooldown => Cooldown;
        }

        [SerializeField] private AttackProjectileData _attackData;

        IAttackThrowingProjectilesData IThrowProjectilesAgentData.AttackData => _attackData;
    }
}