using System.Collections;
using UnityEngine;

namespace Game
{
    public interface IMoveTargetSource
    {
        Vector3 GetTarget(Vector3 fromPosition);
    }

    public class MovementInputTarget : MonoBehaviour, IMoveTargetSource
    {
        [SerializeField] private MoveInputReader _input;
        
        public Vector3 GetTarget(Vector3 fromPosition)
        {
            var moveDir = _input.GetMoveDirection();
            return fromPosition + new Vector3(moveDir.x, 0, moveDir.y);
        }
    }
}