using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ProjectilesLauncher", menuName ="Components/Projectiles Launcher")]
    public class ProjectilesLauncher : ScriptableObject, IProjectileReleaser, IProjectileLauncher
    {
        [SerializeField] private LevelProjectiles _projectiles;
        [SerializeField] private RangeIntersectionsFinder _intersectionFinder; // TODO: to factory
        [SerializeField] private DamageApplier _damageApplier;

        public IList<ProjectileState> Projectiles => _projectiles.Projectiles;
        public event Action<ProjectileState> NewStateEvent;

        public void Launch(Vector3 target, Vector3 origin, ProjectileData projectile, int damage)
        {
            var velocity = CalculateVelocity(target, origin, projectile.FlightTime);
            var projectileState = new ProjectileState(projectile, origin, velocity, damage, projectile.FlightTime);
            _projectiles.AddProjectile(projectileState);
            NewStateEvent?.Invoke(projectileState);
        }

        public void ResetProjectiles()
        {
            _projectiles.ResetState();
        }

        public ProjectileComponent GetProjectileComponent(ProjectileState state)
        {
            return new ProjectileComponent(state, _intersectionFinder, _damageApplier, this);
        }

        private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXz = distance;
            distanceXz.y = 0f;

            float sY = distance.y;
            float sXz = distanceXz.magnitude;

            float Vxz = sXz / time;
            float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

            Vector3 result = distanceXz.normalized;
            result *= Vxz;
            result.y = Vy;

            return result;
        }

        public void Release(ProjectileState state)
        {
            _projectiles.RemoveProjectile(state);
        }
    }

}