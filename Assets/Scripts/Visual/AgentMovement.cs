using System.Collections;
using UnityEngine;
using Game;

namespace Visual
{
    public class AgentMovement : MonoBehaviour
    {
        private IMoveComponent _moveComponent;
        private IAgentsPositionsSurface _positions;

        public void Init(IMoveComponent moveComponent, IAgentsPositionsSurface positions)
        {
            _moveComponent = moveComponent;
            _positions = positions;
            transform.position = _positions.GetNearestPosition(_moveComponent.ProcessMovement(0));
        }

        private void Update()
        {
            if (_moveComponent != null && _moveComponent.IsMovable) // TODO: remove
            {
                var pos = _moveComponent.ProcessMovement(Time.deltaTime);
                if (pos != transform.position)
                {
                    pos = _positions.GetNearestPosition(pos);
                    transform.forward = pos - transform.position;
                    transform.position = pos;
                }
            }
        }

    }
}