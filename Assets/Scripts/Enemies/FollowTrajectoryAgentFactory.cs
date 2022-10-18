using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "FollowTrajectoryAgentFactory", menuName = "Components/Factories/Follow Trajectory Agent")]
    public class FollowTrajectoryAgentFactory : EnemyLogic, IAgentFactory
    {
        [SerializeField] private RangeIntersectionsFinder _intersectionsFinder;
        [SerializeField] private DamageApplier _damageApplier;

        public bool CanProcessData<T>(T data) where T : Agent
        {
            return data is IFollowTrajectoryAutonomousAgentData;
        }

        //private MoveByFollowTrajectory _moveLogic = new MoveByFollowTrajectory();

        public bool CanProcessState<T>(T data) where T : IAutonomousAgentState
        {
            return data is IMoveByFollowTrajectoryState && data is IMoveState;
        }

        public IAutonomousAgentState CreateAgentState<T>(T data, Vector3 startPosition, IList<Vector3> trajectory) where T : Agent
        {
            var followData = data as IFollowTrajectoryAutonomousAgentData;
            return new FollowTrajectoryAgentState(data, followData.MoveData, followData.AttackData, 
                startPosition, trajectory);
        }

        public IMoveComponent CreateMoveComponent<T>(T data) where T : IAutonomousAgentState
        {
            var stateData = data as IFollowTrajectoryAgentState;
            var targeter = new MoveByFollowTrajectory(stateData);
            var mover = new ForwardMover(stateData);
            return new MoveComponentToTarget(targeter, mover);
        }

        public IAttackComponent CreateAttackComponent<T>(T data) where T : IAutonomousAgentState
        {
            var stateData = data as IFollowTrajectoryAgentState;
            var checker = new RangeAttackChecker<IFollowTrajectoryAgentState>(_intersectionsFinder, stateData);
            var applier = new DamageAttackApplier(_damageApplier , stateData);
            var finisher = new DestroyAgentAttackFinish(stateData);
            return new AttackComponent(checker, applier, finisher);
        }
    }
}