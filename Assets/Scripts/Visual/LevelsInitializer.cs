using Game;
using UnityEngine;

namespace Visual
{
    public class LevelsInitializer : MonoBehaviour
    {
        [SerializeField] private LevelPreparer _preparer;

        private void Start()
        {
            _preparer.ChangeFirst();
        }
    }
}