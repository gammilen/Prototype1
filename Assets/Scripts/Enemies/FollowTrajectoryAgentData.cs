using UnityEditor;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class AttackData : IAttackData
    {
        public int Damage;
        public float AttackDistance;
        int IAttackData.Damage => Damage;
        float IAttackData.AttackDistance => AttackDistance;
    }

    [CreateAssetMenu(fileName = "FollowTrajectoryAgentData", menuName ="Entities/Enemies/Follow Trajectory Data")]
    public class FollowTrajectoryAgentData : Agent, IFollowTrajectoryAutonomousAgentData
    {
        [System.Serializable]
        public class MoveData : IMoveData
        {
            public float Speed;
            float IMoveData.Speed => Speed;
        }

        [SerializeField] private MoveData _moveData;
        [SerializeField] private AttackData _attackData;

        IMoveData IFollowTrajectoryAutonomousAgentData.MoveData => _moveData;
        IAttackData IFollowTrajectoryAutonomousAgentData.AttackData => _attackData;
    }
}