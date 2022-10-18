using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName ="Pause", menuName ="Services/Pause")]
    public class Pause : ScriptableObject
    {
        public void StartPause()
        {
            Time.timeScale = 0;
        }

        public void FinishPause()
        {
            Time.timeScale = 1;
        }
    }
}