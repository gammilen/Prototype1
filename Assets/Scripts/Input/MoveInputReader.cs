using UnityEditor;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "MoveInputReader", menuName = "Input/Move Reader")]
    public class MoveInputReader : ScriptableObject
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";

        public Vector2 GetMoveDirection()
        {
            return new Vector2(Input.GetAxis(HORIZONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS)).normalized;
        }
    }
}