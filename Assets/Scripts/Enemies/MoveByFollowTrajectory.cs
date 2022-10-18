using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MoveByFollowTrajectory : IMoveTargeter
    {
        private readonly IMoveByFollowTrajectoryState _state;
        public MoveByFollowTrajectory(IMoveByFollowTrajectoryState state)
        {
            _state = state;
        }
        public Vector3 GetTargetPosition()
        {
            if (TargetIsReached())
            {
                MoveNext();
            }
            return _state.Trajectory[_state.TargetIndex];
        }

        private void MoveNext()
        {
            if (_state.TargetIndex == 0)
            {
                _state.TargetIndex = 1;
                _state.IsForward = true;
                return;
            }
            if (_state.TargetIndex == _state.Trajectory.Count - 1)
            {
                _state.TargetIndex = _state.Trajectory.Count - 2;
                _state.IsForward = false;
                return;
            }
            _state.TargetIndex = _state.TargetIndex + (_state.IsForward ? 1 : -1);
        }

        private bool TargetIsReached()
        {
            return Vector3.SqrMagnitude(_state.Trajectory[_state.TargetIndex] - _state.Position) <= 0.01f;
        }
    }

    

    public interface ITargetsSource
    {
        IEnumerable<IMoveState> GetTargets();
    }

    public class MoveByFollowInSector : IMoveTargeter // TODO: return to initial position (after time?)
    {
        private readonly ITargetsSource _targetsSource;
        private readonly IMoveByFollowInSectorState _state;

        public MoveByFollowInSector(ITargetsSource targetsSource, IMoveByFollowInSectorState state)
        {
            _targetsSource = targetsSource;
            _state = state;
        }

        public Vector3 GetTargetPosition()
        {
            if (_state.Target == null || !InSector(_state.Target.Position))
            {
                MoveNext();
            }
            return _state.Target == null ? _state.Position : _state.Target.Position;
        }

        private void MoveNext()
        {
            _state.Target = null;
            foreach (var agent in _targetsSource.GetTargets())
            {
                if (InSector(agent.Position))
                {
                    _state.Target = agent;
                    _state.Forward = _state.Target.Position - _state.Position;
                    return;
                }
            }
        }

        private bool InSector(Vector3 position)
        {
            return Vector2.Angle(_state.Forward, position - _state.Position) * 2 <= _state.Angle &&
                (position - _state.Position).sqrMagnitude <= _state.Radius * _state.Radius;
        }
    }

    public interface IMoveByFollowInSectorState
    {
        Vector3 Forward { get; set; }
        float Radius { get; }
        float Angle { get; }
        Vector3 Position { get; }
        IMoveState Target { get; set; }
    }

    public class EmptyMove
    {
        public void ProcessMoveTarget()
        {

        }
    }

}