using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // ----- MOVER AND TARGETER ------
    

    public interface IProjectileReleaser
    {
        void Release(ProjectileState state);
    }

    public class EmptyMoveComponent : IMoveComponent
    {
        private IMoveState _moveState;

        public EmptyMoveComponent(IMoveState moveState)
        {
            _moveState = moveState;
        }
        public bool IsMovable => true;

        public Vector3 ProcessMovement(float delta)
        {
            return _moveState.Position;
        }
    }

    public class MoveComponentToTarget : IMoveComponent
    {
        private readonly IMoveTargeter _targeter;
        private readonly IMover _mover;

        public bool IsMovable => true;

        public MoveComponentToTarget(IMoveTargeter targeter, IMover mover)
        {
            _targeter = targeter;
            _mover = mover;
        }


        public Vector3 ProcessMovement(float delta)
        {
            var targetPos = _targeter.GetTargetPosition();
            return _mover.Move(targetPos, delta);
        }
    }

    public class ForwardMover : IMover
    {
        private readonly IMoveState _state;

        public ForwardMover(IMoveState state)
        {
            _state = state;
        }
        public Vector3 Move(Vector3 target, float delta)
        {
            var move = _state.Speed * delta * Vector3.Normalize(target - _state.Position);
            _state.Position += move;
            return _state.Position;
        }

    }

    // ---------ABSTRACT AGENT DATA--------
    public interface IFollowTrajectoryAutonomousAgentData
    {
        IMoveData MoveData { get; }
        IAttackData AttackData { get; }
    }

    public interface IFollowSectorAutonomousAgentData
    {
        ISectorMoveData MoveData { get; }
        IAttackData AttackData { get; }
    }

    public interface IThrowProjectilesAgentData
    {
        IAttackThrowingProjectilesData AttackData { get; }
    }

    public interface IMoveByFollowTrajectoryState
    {
        IList<Vector3> Trajectory { get; }
        int TargetIndex { get; set; }
        bool IsForward { get; set; }
        Vector3 Position { get; }
    }

    // -------DATA------------
    public interface IMoveComponent
    {
        bool IsMovable { get; }
        Vector3 ProcessMovement(float delta);
    }

    public interface IMoveState
    {
        float Speed { get; }
        Vector3 Position { get; set; }
    }

    public interface IMover
    {
        Vector3 Move(Vector3 target, float delta);
    }

    public interface IMoveTargeter
    {
        Vector3 GetTargetPosition();
    }

    public interface ISectorMoveData : IMoveData
    {
        float Angle { get; }
        float Radius { get; }
    }

    public interface IMoveData
    {
        float Speed { get; }
    }



}