using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "FollowSectorAgentData", menuName = "Entities/Enemies/Follow Sector Data")]
    public class FollowSectorAgentData : Agent, IFollowSectorAutonomousAgentData
    {
        [System.Serializable]
        public class MoveData : ISectorMoveData
        {
            public float Speed;
            public float Angle;
            public float Radius;
            float IMoveData.Speed => Speed;
            float ISectorMoveData.Angle => Angle;
            float ISectorMoveData.Radius => Radius;
        }

        [SerializeField] private MoveData _moveData;
        [SerializeField] private AttackData _attackData;

        ISectorMoveData IFollowSectorAutonomousAgentData.MoveData => _moveData;
        IAttackData IFollowSectorAutonomousAgentData.AttackData => _attackData;
    }
}