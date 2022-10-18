using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Game
{
    [System.Serializable]
    public struct Vector3Info
    {
        public float x;
        public float y;
        public float z;

        public Vector3Info(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }
    }

    public static class Vector3Extensions
    {
        public static Vector3 ToVector(this Vector3Info info)
        {
            return new Vector3(info.x, info.y, info.z);
        }
    }

    public interface IFollowTrajectoryAgentState : IAutonomousAgentState, IMoveByFollowTrajectoryState, IMoveState, IAttackState, IDamageableState
    {

    }

    [System.Serializable]
    public class FollowTrajectoryAgentState : IFollowTrajectoryAgentState
    {
        [JsonConverter(typeof(SOConverter<Agent>))]
        [field: SerializeField] public Agent Data { get; private set; }
        [JsonConverter(typeof(Vector3ListConverter))]
        public IList<Vector3> Trajectory { get; private set; }
        public int TargetIndex { get; set; }
        public bool IsForward { get; set; }
        public float Speed { get; private set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Position { get; set; }
        public bool IsDead { get; private set; }
        public int Damage { get; private set; }
        public float AttackDistance { get; private set; }

        public FollowTrajectoryAgentState(Agent data, IMoveData moveData, IAttackData attackData,
            Vector3 startPosition, IList<Vector3> trajectory)
        {
            Speed = moveData.Speed;
            Data = data;
            Position = startPosition;
            Trajectory = trajectory;
            Damage = attackData.Damage;
            AttackDistance = attackData.AttackDistance;
        }

        [JsonConstructor]
        public FollowTrajectoryAgentState(Agent data, IList<Vector3> trajectory, int targetIndex,
            bool isForward, float speed, Vector3 position, bool isDead, int damage, float attackDistance)
        {
            Data = data;
            TargetIndex = targetIndex;
            Trajectory = trajectory;
            IsForward = isForward;
            Speed = speed;
            Position = position;
            IsDead = isDead;
            Damage = damage;
            AttackDistance = attackDistance;
        }

        public void SetDead()
        {
            IsDead = true;
        }
    }

    public interface IFollowSectorAgentState : IAutonomousAgentState, IMoveState, IAttackState, IMoveByFollowInSectorState, IDamageableState
    {    }

    [System.Serializable]
    public class FollowSectorAgentState : IFollowSectorAgentState
    {
        [JsonConverter(typeof(SOConverter<Agent>))]
        [field: SerializeField] public Agent Data { get; private set; }
        public float Speed { get; private set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Position { get; set; }
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Forward { get; set; }
        public float Radius { get; private set; }
        public float Angle { get; private set; }
        public bool IsDead { get; private set; }
        [JsonIgnore] public IMoveState Target { get; set; }
        public int Damage { get; private set; }
        public float AttackDistance { get; private set; }

        public FollowSectorAgentState(Agent data, ISectorMoveData moveData, IAttackData attackData, 
            Vector3 startPosition)
        {
            Data = data;
            Speed = moveData.Speed;
            Radius = moveData.Radius;
            Angle = moveData.Angle;
            Position = startPosition;
            Damage = attackData.Damage;
            AttackDistance = attackData.AttackDistance;
        }

        [JsonConstructor]
        public FollowSectorAgentState(Agent data, float speed, Vector3 position, Vector3 forward, float radius,
            float angle, bool isDead, int damage, float attackDistance)
        {
            Data = data;
            Speed = speed;
            Position = position;
            Forward = forward;
            Radius = radius;
            Angle = angle;
            IsDead = isDead;
            Damage = damage;
            AttackDistance = attackDistance;
        }

        public void SetDead()
        {
            IsDead = true;
        }
    }

    public interface IThrowProjectilesAgentState : IAutonomousAgentState, IMoveState, IAttackState, IAttackCooldownState
    {
        ProjectileData Projectile { get; }
    }

    [System.Serializable]
    public class ThrowProjectilesAgentState : IThrowProjectilesAgentState
    {
        [JsonConverter(typeof(SOConverter<Agent>))]
        public Agent Data { get; private set; }
        [JsonConverter(typeof(SOConverter<ProjectileData>))]
        public ProjectileData Projectile { get; private set; }
        public float Speed => 0;
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 Position { get; set; }
        public bool IsDead => false;
        public int Damage { get; private set; }
        public float AttackDistance { get; private set; }
        public float Cooldown { get; set; }
        public float OriginalCooldown { get; private set; }


        public ThrowProjectilesAgentState(Agent data, IAttackThrowingProjectilesData attackData,
            Vector3 startPosition)
        {
            Data = data;
            Position = startPosition;
            Damage = attackData.Damage;
            AttackDistance = attackData.AttackDistance;
            OriginalCooldown = Cooldown = attackData.Cooldown;
            Projectile = attackData.Projectile;
        }

        [JsonConstructor]
        public ThrowProjectilesAgentState(Agent data, ProjectileData projectile, Vector3 position,
            int damage, float attackDistance, float cooldown, float originalCooldown)
        {
            Data = data;
            Projectile = projectile;
            Position = position;
            Damage = damage;
            AttackDistance = attackDistance;
            Cooldown = cooldown;
            OriginalCooldown = originalCooldown;
        }

        public void SetCooldown()
        {
            Cooldown = OriginalCooldown;
        }
    }



    public interface IAutonomousAgentState
    {
        Agent Data { get; }
        bool IsDead { get; }
    }
}