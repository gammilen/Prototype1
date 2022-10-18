using System.Collections;
using UnityEngine;
using Game;

namespace Visual
{
    public interface IAgentsPositionsSurface
    {
        Vector3 GetNearestPosition(Vector3 position);
        Vector3 MoveTowards(Vector3 fromPosition, Vector3 toPosition, float distance);

    }

    public interface IEscapable
    {
        void Escape();
    }

    public class PlayableAgent : MonoBehaviour, IEscapable
    {
        private IMoveTargetSource _moveTarget;
        private IAgentsPositionsSurface _positions;
        private IEscapeProcessor _escapeProcessor;
        private IAgentMoveHandler _moveHandler;
        private PlayableAgentState _agentState;
        

        private void Start()
        {
            _moveTarget = GetComponent<IMoveTargetSource>();
        }

        private void Update()
        {
            if (!_agentState.IsActive)
            {
                return;
            }
            var pos = _positions.MoveTowards(transform.position, _moveTarget.GetTarget(transform.position), _agentState.Speed * Time.deltaTime);
            transform.forward = pos - transform.position;
            transform.position = pos;
            _moveHandler.HandleMove(_agentState, transform.position);
        }

        public void Init(PlayableAgentState agentState, IAgentsPositionsSurface positions, 
            IEscapeProcessor escapeProcessor, IAgentMoveHandler moveHandler)
        {
            _agentState = agentState;
            _positions = positions;
            _escapeProcessor = escapeProcessor;
            _moveHandler = moveHandler;
            transform.position = _positions.GetNearestPosition(_agentState.Position);
            _agentState.SetPosition(transform.position);
        }

        public void Escape()
        {
            _escapeProcessor.Escape(_agentState);
        }
    }
}