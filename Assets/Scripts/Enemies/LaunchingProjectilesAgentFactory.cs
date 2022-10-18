using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LaunchingProjectilesAgentFactory", menuName = "Components/Factories/Launching Projectiles Agent")]
    public class LaunchingProjectilesAgentFactory : EnemyLogic, IAgentFactory
    {
        [SerializeField] private RangeIntersectionsFinder _intersectionsFinder;
        [SerializeField] private ProjectilesLauncher _projectilesLauncher;
        [SerializeField] private DamageApplier _damageApplier;

        public bool CanProcessData<T>(T data) where T : Agent
        {
            return data is IThrowProjectilesAgentData;
        }

        public bool CanProcessState<T>(T data) where T : IAutonomousAgentState
        {
            return data is IThrowProjectilesAgentState;
        }

        public IAutonomousAgentState CreateAgentState<T>(T data, Vector3 startPosition, IList<Vector3> trajectory) where T : Agent
        {
            var throwData = data as IThrowProjectilesAgentData;
            return new ThrowProjectilesAgentState(data, throwData.AttackData, startPosition);
        }

        public IMoveComponent CreateMoveComponent<T>(T data) where T : IAutonomousAgentState
        {
            return new EmptyMoveComponent(data as IMoveState);
        }

        public IAttackComponent CreateAttackComponent<T>(T data) where T : IAutonomousAgentState
        {
            var stateData = data as IThrowProjectilesAgentState;
            var checker = new RangeWithCooldownAttackChecker<IThrowProjectilesAgentState>(_intersectionsFinder, stateData);
            var applier = new ProjectileAttackApplier<IThrowProjectilesAgentState>(_projectilesLauncher, stateData, stateData.Projectile);
            var finisher = new SetCooldownAttackFinish(stateData);
            return new AttackComponent(checker, applier, finisher);
        }
    }
}