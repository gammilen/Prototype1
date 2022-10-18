using System;
using UnityEngine;

namespace Game
{
    public class ProjectileComponent
    {
        private ProjectileState _state;
        private IRangeIntersectionsObserver _intersectionObserver;
        private IDamageApplier _damageApplier;
        private IProjectileReleaser _releaser;
        private bool _released;
        public event Action ReleaseEvent;

        public ProjectileComponent(ProjectileState state, IRangeIntersectionsObserver intersectionObserver,
            IDamageApplier damageApplier, IProjectileReleaser releaser)
        {
            _state = state;
            _intersectionObserver = intersectionObserver;
            _damageApplier = damageApplier;
            _releaser = releaser;
        }

        public Vector3 Process(float delta)
        {
            if (_released)
            {
                throw new Exception("Projectile already released");
            }
            _state.ProgressTime += delta;
            if (_state.ProgressTime > _state.OriginalTime)
            {
                ProcessCollision();
                return _state.Position;
            }
            _state.Position = CalculatePosInTime(_state.Velocity, _state.OriginPosition, _state.ProgressTime);
            if (_intersectionObserver.TryGetIntersection(_state.Position, _state.Data.CollisionDistance, out var agent))
            {
                _damageApplier.Apply(_state.Damage, agent);
                ProcessCollision();
            }
            return _state.Position;
        }

        public void ProcessCollision()
        {
            _released = true;
            _releaser.Release(_state);
            ReleaseEvent?.Invoke();
        }

        private Vector3 CalculatePosInTime(Vector3 vo, Vector3 origin, float time)
        {
            Vector3 Vxz = vo;
            Vxz.y = 0f;

            Vector3 result = origin + vo * time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + origin.y;

            result.y = sY;

            return result;
        }

    }
}