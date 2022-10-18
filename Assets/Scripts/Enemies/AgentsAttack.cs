using System;
using UnityEngine;

namespace Game
{
    public interface IAttackComponent
    {
        event Action AttackStarted;
        event Action AttackFinished;
        event Action Destroyed;
        void Attack(float delta);
    }

    public class AttackComponent : IAttackComponent
    {
        private IAttackChecker _checker;
        private IAttackApplier _applier;
        private IAttackFinisher _finisher;

        public event Action AttackStarted;
        public event Action AttackFinished;
        public event Action Destroyed;

        public AttackComponent(IAttackChecker checker, IAttackApplier applier, IAttackFinisher finisher)
        {
            _checker = checker;
            _applier = applier;
            _finisher = finisher;
        }

        public void Attack(float delta)
        {
            if (!_finisher.IsDestroyed)
            {
                var agent = _checker.Check(delta);
                if (agent != null)
                {
                    StartAttack(agent);
                }
            }
        }

        private void StartAttack(IPlayableAgent agent)
        {
            AttackStarted?.Invoke();
            _applier.Apply(agent);
            FinishAttack();
        }

        private void FinishAttack()
        {
            _finisher.Finish();
            AttackFinished?.Invoke();
            if (_finisher.IsDestroyed)
            {
                Destroyed?.Invoke();
            }
        }
    }

    public interface IDamageApplier
    {
        void Apply(int amount, IPlayableAgent playableAgent);
    }

    

    public interface IAttackChecker
    {
        IPlayableAgent Check(float delta);
    }

    public interface IMoveAttackState : IMoveState, IAttackState
    { }

    public class RangeAttackChecker<T> : IAttackChecker
        where T : IMoveState, IAttackState
    {
        private IRangeIntersectionsObserver _intersectionsObserver;
        private T _agent;

        public event Action<IPlayableAgent> CheckEvent;
        
        public RangeAttackChecker(IRangeIntersectionsObserver intersectionsObserver, T agent)
        {
            _agent = agent;
            _intersectionsObserver = intersectionsObserver;
        }

        public IPlayableAgent Check(float delta)
        {
            if (_intersectionsObserver.TryGetIntersection(_agent.Position, _agent.AttackDistance, out var playableAgent))
            {
                return playableAgent;
            }
            return null;
        }
    }

    public class RangeWithCooldownAttackChecker<T> : IAttackChecker
        where T : IMoveState, IAttackState, IAttackCooldownState
    {
        private IRangeIntersectionsObserver _intersectionsObserver;
        private T _agent;

        public event Action<IPlayableAgent> CheckEvent;

        public RangeWithCooldownAttackChecker(IRangeIntersectionsObserver intersectionsObserver, T agent)
        {
            _agent = agent;
            _intersectionsObserver = intersectionsObserver;
        }

        public IPlayableAgent Check(float delta)
        {
            _agent.Cooldown -= delta;
            if (_intersectionsObserver.TryGetIntersection(_agent.Position, _agent.AttackDistance, out var playableAgent) &&
                CheckCooldown())
            {
                return playableAgent;
            }
            return null;
        }

        private bool CheckCooldown()
        {
            return _agent.Cooldown <= 0;
        }
    }

    

    public interface IAttackApplier
    {
        void Apply(IPlayableAgent agent);
    }

    public class DamageAttackApplier : IAttackApplier
    {
        private IDamageApplier _damageApplier;
        private IAttackState _attackState;
        public DamageAttackApplier(IDamageApplier damageApplier, IAttackState damageState)
        {
            _damageApplier = damageApplier;
            _attackState = damageState;
        }

        public void Apply(IPlayableAgent agent)
        {
            _damageApplier.Apply(_attackState.Damage, agent);
        }
    }

    public class ProjectileAttackApplier<T> : IAttackApplier
        where T : IAttackState, IMoveState
    {
        private IProjectileLauncher _projectilesLauncher;
        private ProjectileData _projectile;
        private T _state;
        public ProjectileAttackApplier(IProjectileLauncher projectilesLauncher, T state, ProjectileData projectile)
        {
            _projectilesLauncher = projectilesLauncher;
            _projectile = projectile;
            _state = state;
        }

        public void Apply(IPlayableAgent agent)
        {
            _projectilesLauncher.Launch((agent as IMoveState).Position, _state.Position, _projectile, _state.Damage);
        }
    }

    public interface IProjectileLauncher
    {
        void Launch(Vector3 target, Vector3 origin, ProjectileData projectile, int damage);
    }

    public interface IAttackFinisher
    {
        void Finish();
        bool IsDestroyed { get; }
    }

    public class DestroyAgentAttackFinish : IAttackFinisher
    {
        public IDamageableState _state;
        public bool IsDestroyed { get; private set; }

        public DestroyAgentAttackFinish(IDamageableState state)
        {
            _state = state;
        }

        public void Finish()
        {
            _state.SetDead();
            IsDestroyed = true;
        }
    }

    public class SetCooldownAttackFinish : IAttackFinisher
    {
        public IAttackCooldownState _state;
        public bool IsDestroyed => false;

        public SetCooldownAttackFinish(IAttackCooldownState state)
        {
            _state = state;
        }

        public void Finish()
        {
            _state.SetCooldown();
        }
    }
    

    public interface IAgentsCollisionsObserver // TODO: remove?
    {
        public delegate void CollisionAction();
        void AddCollisionsListener(IMoveState agent, CollisionAction action);
    }

    public interface IRangeIntersectionsObserver
    {
        public delegate void IntersectionAction(IPlayableAgent agent);

        void AddIntersectionListener(IMoveState agent, float range, IntersectionAction action);
        bool TryGetIntersection(Vector3 position, float range, out IPlayableAgent playableAgent);
    }


    public interface IAttackData
    {
        int Damage { get; }
        float AttackDistance { get; }
    }

    public interface IAttackThrowingProjectilesData : IAttackData, IAttackCooldown
    {
        ProjectileData Projectile { get; }
    }

    public interface IAttackState
    {
        int Damage { get; }
        float AttackDistance { get; }
    }

    public interface IAttackCooldown
    {
        float Cooldown { get; }
    }

    public interface IAttackCooldownState
    {
        float Cooldown { get; set; }
        void SetCooldown();
    }

    public interface IDamageableState
    {
        bool IsDead { get; }
        void SetDead();
    }
}