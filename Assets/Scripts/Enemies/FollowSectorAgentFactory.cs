using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "FollowSectorAgentFactory", menuName = "Components/Factories/Follow Sector Agent")]
    public class FollowSectorAgentFactory : EnemyLogic, IAgentFactory
    {
        [SerializeField] private LevelAgentsRegistry _registry;
        [SerializeField] private RangeIntersectionsFinder _intersectionsFinder;
        [SerializeField] private DamageApplier _damageApplier;

        public bool CanProcessData<T>(T data) where T : Agent
        {
            return data is IFollowSectorAutonomousAgentData;
        }

        public bool CanProcessState<T>(T data) where T : IAutonomousAgentState
        {
            return data is IMoveByFollowInSectorState;
        }

        public IAutonomousAgentState CreateAgentState<T>(T data, Vector3 startPosition, IList<Vector3> trajectory) where T : Agent
        {
            var followSectorData = data as IFollowSectorAutonomousAgentData;
            return new FollowSectorAgentState(data, followSectorData.MoveData, followSectorData.AttackData, startPosition);
        }

        public IMoveComponent CreateMoveComponent<T>(T data) where T : IAutonomousAgentState
        {
            var targeter = new MoveByFollowInSector(_registry, data as IMoveByFollowInSectorState);
            var mover = new ForwardMover(data as IMoveState);
            return new MoveComponentToTarget(targeter, mover);
        }

        //TODO: is a copy of trajectory factory
        public IAttackComponent CreateAttackComponent<T>(T data) where T : IAutonomousAgentState
        {
            var stateData = data as IFollowSectorAgentState;
            var checker = new RangeAttackChecker<IFollowSectorAgentState>(_intersectionsFinder, stateData);
            var applier = new DamageAttackApplier(_damageApplier, stateData);
            var finisher = new DestroyAgentAttackFinish(stateData);
            return new AttackComponent(checker, applier, finisher);
        }
    }
}