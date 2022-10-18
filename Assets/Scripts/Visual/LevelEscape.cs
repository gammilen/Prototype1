using System.Collections;
using UnityEngine;
using Game;

namespace Visual
{
    [RequireComponent(typeof(Collider))]
    public class LevelEscape : MonoBehaviour
    {
        [SerializeField] private LevelPlayArea _playArea;
        [SerializeField] private InGameLevel _level;

        private void Start()
        {
            transform.position = _playArea.GetNearestPosition(
                new PointsPositionResolver().ResolvePoint(_level.CurrentLevel, _level.CurrentLevel.Map.EscapePoint));


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<IEscapable>(out var escapable))
            {
                escapable.Escape();
            }
        }

        public void Init()
        {

        }
    }
}